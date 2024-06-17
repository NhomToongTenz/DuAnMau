using UI;
using UnityEngine;

namespace SkillManager
{
    public class Dodge_Skill : Skill
    {
        [Header("Dodge")]
        [SerializeField] private UI_SkillTreeSLot dodgeUnlockBtn;
        [SerializeField] private int evasionAmount;
        public bool dodgeUnlocked{get; private set;}
        
        [Header("Mirage Dodge")]
        [SerializeField] private UI_SkillTreeSLot dodgeMirageBtn;
        public bool dodgeMirageUnlocked{get; private set;}

        protected override void Start()
        {
            base.Start();
            
            dodgeUnlockBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockDodge);
            dodgeMirageBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UnlockDodgeMirage);
        }
        
        void UnlockDodge()
        {
            if (dodgeUnlockBtn.unlocked)
            {
                player.stats.evasion.AddModifier(evasionAmount);
                Inventory.instance.UpdateStatsUI();
                dodgeUnlocked = true;
                
            }
        }
        
        void UnlockDodgeMirage()
        {
            if(dodgeMirageBtn.unlocked)
                dodgeMirageUnlocked = true;
        }
        
       public void CreateMirageOnDoDodge()
        {
            if(dodgeMirageUnlocked)
            {
                // create mirage
                SkillManager.instance.cloneSkill.CreateClone(player.transform, new Vector3(2 * player.facingDirection,0));
            }
        }
    }
}