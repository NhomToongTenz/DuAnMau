using TMPro;
using UnityEngine;

namespace UI
{
    public class UI_SkillToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillDescription;
        public void ShowToolTip(string skillDescription, string skillName)
        {
            this.skillName.text = skillName;
            this.skillDescription.text = skillDescription;
            gameObject.SetActive(true);
        }
        
        public void HideToolTIp()
        {
            gameObject.SetActive(false);
        }
    }
}