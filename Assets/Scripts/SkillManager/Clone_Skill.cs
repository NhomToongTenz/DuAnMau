using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace SkillManager
{

    public class Clone_Skill : Skill
    {
        [Header("Clone info")]
        [SerializeField] private float attackMultiplier;
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;
        [Space] 
        
        [Header("Clone attack")]
        [SerializeField] private UI_SkillTreeSLot cloneAttackUnlockBtn;
        [SerializeField] private float cloneAttackMultiplier;
        [SerializeField] private bool canAttack;
        
        [Header("Aggressive clone")]
        [SerializeField] private UI_SkillTreeSLot aggressiveCloneUnlockBtn;
        [SerializeField] private float aggressiveCloneMultiplier;
        public bool canApplyOnHitEffect{get; private set;}
        
        
        [Header("Multiple clone")]
        [SerializeField] private UI_SkillTreeSLot multipleCloneUnlockBtn;
        [SerializeField] private float multipleCloneMultiplier;
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate = 35;
        [Header("Crystals instead of clones")] 
        [SerializeField] private UI_SkillTreeSLot crystalInsteadUnlockBtn;
        public bool useCrystalsInsteadOfClones;


        protected override void Start()
        {
            base.Start();
            
            cloneAttackUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCLoneAttack);
            aggressiveCloneUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockAggressiveClone);
            multipleCloneUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockMultipleClone);
            crystalInsteadUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCrystalInstead);
        }

        #region Unlock region

        private void UnlockCLoneAttack()
        {
            if(cloneAttackUnlockBtn.unlocked)
            {
                canAttack = true;
                attackMultiplier = cloneAttackMultiplier;
            }
        }
        
        private void UnlockAggressiveClone()
        {
            if(aggressiveCloneUnlockBtn.unlocked)
            {
                canApplyOnHitEffect = true;
                attackMultiplier = aggressiveCloneMultiplier;
            }
        }
        
        private void UnlockMultipleClone()
        {
            if(multipleCloneUnlockBtn.unlocked)
            {
                canDuplicateClone = true;
                attackMultiplier = multipleCloneMultiplier;
            }
        }   
        
        private void UnlockCrystalInstead()
        {
            if(crystalInsteadUnlockBtn.unlocked)
            {
                useCrystalsInsteadOfClones = true;
            }
        }
        
        
        #endregion
        
        
        
        
        public void CreateClone(Transform _clonPos, Vector3 _offset)
        {

            if (useCrystalsInsteadOfClones)
            {
                SkillManager.instance.crystalSkill.CreateCrystal();
                SkillManager.instance.crystalSkill.CurrentCrystalChooseRandomTarget();
                return;
            }


            GameObject newClone = Instantiate(clonePrefab);

            newClone.GetComponent<Clone_Skill_Controller>()
                .SetupClone(_clonPos, cloneDuration, canAttack, _offset,
                    FindClosestEnemy(newClone.transform), canDuplicateClone, chanceToDuplicate, player, attackMultiplier);

        }

        public void CreateCloneWithDelay(Transform _clonPos)
        {
           StartCoroutine(CloneDelayCorotine(_clonPos, new Vector3(2 * player.facingDirection, 0, 0)));
        }

        private IEnumerator CloneDelayCorotine(Transform _clonPos, Vector3 _offset)
        {
            yield return new WaitForSeconds(0.4f);
            CreateClone(_clonPos, _offset);
            //CreateClone(player.transform, Vector3.zero);
        }
    }
}