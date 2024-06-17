using UI;
using UnityEngine;
using UnityEngine.UI;

namespace SkillManager
{
    public class Parry_Skill : Skill
    {
        [Header("Parry")]
        [SerializeField] private UI_SkillTreeSLot parryUnlockBtn;
        public bool parryUnlocked{get; private set;}
        
        [Header("Parry restore")]
        [SerializeField] private UI_SkillTreeSLot parryRestoreBtn;
        [Range(0f,1f)]
        [SerializeField] private float healthRestorePercentage;
        public bool parryRestoreUnlocked{get; private set;}
        
        [Header("Parry with mirage")]
        [SerializeField] private UI_SkillTreeSLot parryMirageBtn;
        public bool parryMirageUnlocked{get; private set;}
        
        
        public override void UseSkill()
        {
            base.UseSkill();
            if (parryRestoreUnlocked)
            {
                int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHeathValue() * healthRestorePercentage);
                player.stats.IncreaseHealthBy(restoreAmount);
                
            }
        }

        protected override void Start()
        {
            base.Start();
            
            parryUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockParry);
            parryRestoreBtn.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
            parryMirageBtn.GetComponent<Button>().onClick.AddListener(UnlockParryMirage);
        }
        
        public void UnlockParry()
        {
            if(parryUnlockBtn.unlocked)
                parryUnlocked = true;
        }
        
        public void UnlockParryRestore()
        {
            if(parryRestoreBtn.unlocked)
                parryRestoreUnlocked = true;
        }
        
        public void UnlockParryMirage()
        {
            if(parryMirageBtn.unlocked)
                parryMirageUnlocked = true;
        }

        public void MakeMirageOnParry(Transform _respawnTransform)
        {
            if (parryMirageUnlocked)
            {
                SkillManager.instance.cloneSkill.CreateCloneWithDelay(_respawnTransform);
            }
        }
        
    }
}