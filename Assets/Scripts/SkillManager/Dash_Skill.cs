
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace SkillManager
{

    public class Dash_Skill : Skill
    {
        [Header("Dash")] public bool dashUnlocked{get; private set  ;}
        [SerializeField] private UI_SkillTreeSLot dashUnlockBtn;

        [Header("Clone on dash")] public bool cloneOnDashUnlocked{get; private set;}
        [SerializeField] private UI_SkillTreeSLot cloneOnDashUnlockBtn;

        [Header("Clone on arrival")] public bool cloneOnArrivalUnlocked{get; private set;}
        [SerializeField] private UI_SkillTreeSLot cloneOnArrivalUnlockBtn;

        public override void UseSkill()
        {
            base.UseSkill();

        }


        protected override void Start()
        {
            base.Start();

            dashUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockDash);
            cloneOnDashUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
            cloneOnArrivalUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
        }

        public void CloneOnDash()
        {
            if (cloneOnDashUnlocked)
                SkillManager.instance.cloneSkill.CreateClone(player.transform, Vector3.zero);
        }

        public void CloneOnDashArrival()
        {
            if (cloneOnArrivalUnlocked)
                SkillManager.instance.cloneSkill.CreateClone(player.transform, Vector3.zero);
        }

        private void UnlockDash()
        {

            if (dashUnlockBtn.unlocked)
                dashUnlocked = true;

            //dashUnlockBtn.Unlock();
        }

        private void UnlockCloneOnDash()
        {
            if (cloneOnDashUnlockBtn.unlocked)
                cloneOnDashUnlocked = true;
            //cloneOnDashUnlockBtn.Unlock();
        }

        private void UnlockCloneOnArrival()
        {
            if (cloneOnArrivalUnlockBtn.unlocked)
                cloneOnArrivalUnlocked = true;
            //cloneOnArrivalUnlockBtn.Unlock();
        }

    }
}