using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UI_SkillTreeSLot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UI ui;
        private Image skillTreeImage;
        
        [SerializeField] private int skillCost;
        [SerializeField] private string skillName;
        [TextArea]
        [SerializeField] private string skillDescription;
        
        [SerializeField] private Color lockedSkillColor = Color.gray;
        
        
        public bool unlocked;
        
        [SerializeField] private UI_SkillTreeSLot[] shouldUnlockSlots;
        [SerializeField] private UI_SkillTreeSLot[] shouldLockSlots;
        

        private void OnValidate()
        {
            gameObject.name = "Skill tree slot: " + skillName;
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => UnlockSKillSlot());
        }

        private void Start()
        {   
           skillTreeImage = GetComponent<Image>();
           ui = GetComponentInParent<UI>();
           
           skillTreeImage.color = unlocked ? Color.white : Color.gray;
           
        }

        public void UnlockSKillSlot()
        {   
            
            if(!PlayerManager.instance.HaveEnoughMoney(skillCost) || unlocked)
                return;
            
            for (int i = 0; i < shouldUnlockSlots.Length; i++)
            {
                if(!shouldUnlockSlots[i].unlocked)
                {   
                    Debug.Log("You need to unlock the previous skill first");
                    return;
                }
            }
            
            for (int i = 0; i < shouldLockSlots.Length; i++)
            {
                if(shouldLockSlots[i].unlocked)
                {
                    Debug.Log("You need to unlock the previous skill first");
                    return;
                }
            }
            
            unlocked = true;
            skillTreeImage.color = Color.white;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ui.skillToolTip.ShowToolTip(skillDescription, skillName);
            
            Vector2 mousePosition = Input.mousePosition;

            float xOffset = 0;
            float yOffset = 0;
            
            if(mousePosition.x > 600)
                xOffset = -150;
            else
                xOffset = 150;
            
            if (mousePosition.y > 320)
                yOffset = 150;
            else
                yOffset = -150;
            
            
            ui.skillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.skillToolTip.HideToolTIp();
        }
    }
}