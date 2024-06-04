using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject characterUI;
        [SerializeField] private GameObject skillTreeUI;
        [SerializeField] private GameObject craftUI; 
        [SerializeField] private GameObject optionsUI;
        [SerializeField] private GameObject inGameUI;
        
        public UI_ItemTooltip itemTooltip;
        public UI_StatToolTip statToolTip;
        public UI_CraftWindow craftWindow;
        public UI_SkillToolTip skillToolTip;

        private void Awake()
        {
            Switchto(skillTreeUI); // we need this to assign event on skill tree slot before we assign event on skill scripts
        }

        private void Start()
        {
          //  itemTooltip = GetComponentInChildren<UI_ItemTooltip>();
          
          Switchto(inGameUI);
          
          itemTooltip.gameObject.SetActive(false);
          statToolTip.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
                SwitchWithKeyTo(characterUI);
            
            if(Input.GetKeyDown(KeyCode.B))
                SwitchWithKeyTo(craftUI);
            
            if(Input.GetKeyDown(KeyCode.K))
                SwitchWithKeyTo(skillTreeUI);
            
            if(Input.GetKeyDown(KeyCode.O))
                SwitchWithKeyTo(optionsUI);
            
            
        }

        public void Switchto(GameObject _menu)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);

            if (_menu != null)
                _menu.SetActive(true);
        }
        
        public void SwitchWithKeyTo(GameObject _menu)
        {
            if (_menu != null && _menu.activeSelf)
            {
                _menu.SetActive(false);
                CheckForInGameUI();
                return;
            }
            Switchto(_menu);
        }

        private void CheckForInGameUI()
        {
            for (int i = 0; i < transform.childCount; i++)
                if(transform.GetChild(i).gameObject.activeSelf)
                    return;
            
            Switchto(inGameUI);
        }
    }
}
