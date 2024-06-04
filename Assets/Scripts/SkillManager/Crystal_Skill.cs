using System.Collections.Generic;
using UI;
using UnityEngine;

namespace SkillManager
{

    public class Crystal_Skill : Skill
    {
        [SerializeField] private GameObject crystalPrefab;
        private GameObject currentCrystal;
        [SerializeField] private float crystalExistTimer;

        [Header("Crystal Mirages")] 
        [SerializeField] private UI_SkillTreeSLot crystalMirageBtn; // cloneToInsteadOfCrystal
      [SerializeField] private bool cloneInsteadOfCrystal;
        
        [Header("Crystal simple")] [SerializeField] private UI_SkillTreeSLot crystalUnlockBtn;
        
        public bool crystalUnlocked { get; private set; }

        [Header("Explosion Crystal")] 
        [SerializeField] private UI_SkillTreeSLot crystalExplosionBtn;
        [SerializeField]
        private bool canExplode;
        

        [Header("Moving Crystal")] [SerializeField]
        private float crystalSpeed;
        [SerializeField] private UI_SkillTreeSLot crystalMoveBtn;
        [SerializeField] private bool canMoveToEnemy;

        [Header("Multiple Crystals")] [SerializeField]
        private bool canHaveMultipleCrystals;
        [SerializeField] private UI_SkillTreeSLot crystalMultipleBtn;

        [SerializeField] private int amountOfStacks;
        [SerializeField] private float timeBetweenStacks;
        [SerializeField] private float useTimeWindow;
        [SerializeField] private List<GameObject> crystalsLeft = new List<GameObject>();


        protected override void Start()
        {
            base.Start();
            
            crystalUnlockBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockCrystal);
            crystalExplosionBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockCrystalExplosion);
            crystalMoveBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockCrystalMove);
            crystalMirageBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockCrystalMirage);
            crystalMultipleBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockCrystalMultiple);
            
        }

        // unlock crystal skill
        #region UnlockSkillRegion
        
        public void UnlockCrystal()
        {
            if (crystalUnlockBtn.unlocked)
                crystalUnlocked = true;
        }
        
        public void UnlockCrystalExplosion()
        {
            if (crystalExplosionBtn.unlocked)
                canExplode = true;
        }
        
        public void UnlockCrystalMove()
        {
            if (crystalMoveBtn.unlocked)
                canMoveToEnemy = true;
        }
        
        public void UnlockCrystalMirage()
        {
            if (crystalMirageBtn.unlocked)
                cloneInsteadOfCrystal = true;
        }
        
        public void UnlockCrystalMultiple()
        {
            if (crystalMultipleBtn.unlocked)
                canHaveMultipleCrystals = true;
        }
        #endregion
        
        

        public override void UseSkill()
        {
            base.UseSkill();

            //RefilCrytalStacks();
            if (CanUseMultiCrystal())
                return;

            if (currentCrystal == null)
            {
                CreateCrystal();
            }
            else
            {
                if (canMoveToEnemy)
                    return;

                Vector2 temp = player.transform.position;
                player.transform.position = currentCrystal.transform.position;
                currentCrystal.transform.position = temp;

                if (cloneInsteadOfCrystal)
                {
                    SkillManager.instance.cloneSkill.CreateClone(currentCrystal.transform, Vector3.zero);
                    Destroy(currentCrystal);
                }
                else
                {
                    currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
                }
            }
        }

        public void CreateCrystal()
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            Crystal_Skill_Controller currentCrystalController = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCrystalController.SetupCrystal(crystalExistTimer, canExplode, canMoveToEnemy, crystalSpeed,
                FindClosestEnemy(currentCrystal.transform), player);
            //if()
            currentCrystalController.ChooseRandomEnemy();
        }

        public void CurrentCrystalChooseRandomTarget() =>
            currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

        private void RefilCrytalStacks()
        {
            int amountToAdd = amountOfStacks - crystalsLeft.Count;
            for (int i = 0; i < amountToAdd; i++)
            {
                //GameObject crystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
                crystalsLeft.Add(crystalPrefab);
            }
        }

        private bool CanUseMultiCrystal()
        {
            if (canHaveMultipleCrystals)
            {
                if (crystalsLeft.Count > 0)
                {
                    if (crystalsLeft.Count == amountOfStacks)
                        Invoke("ResetAbility", useTimeWindow);

                    coolDown = 0;
                    GameObject crystalSpawn = crystalsLeft[crystalsLeft.Count - 1];
                    GameObject newCrystal = Instantiate(crystalSpawn, player.transform.position, Quaternion.identity);

                    crystalsLeft.Remove(crystalSpawn);

                    newCrystal.GetComponent<Crystal_Skill_Controller>()
                        .SetupCrystal(crystalExistTimer, canExplode,
                            canMoveToEnemy, crystalSpeed,
                            FindClosestEnemy(newCrystal.transform), player);

                    if (crystalsLeft.Count <= 0)
                    {
                        //coolDown 
                        coolDown = timeBetweenStacks;
                        RefilCrytalStacks();
                        //Invoke("RefilCrytalStacks", timeBetweenStacks);
                    }

                    return true;
                }

            }

            return false;
        }

        private void ResetAbility()
        {
            if (cooldownTimer > 0)
                return;
            cooldownTimer = crystalExistTimer;
            RefilCrytalStacks();
        }


    }
}