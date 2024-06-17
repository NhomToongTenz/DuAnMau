using TMPro;
using UnityEngine;

namespace UI
{
    public class UI_StatToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        public void ShowToolTip(string _description)
        {
            descriptionText.text = _description;
            gameObject.SetActive(true);
        }
        
        public void HideToolTip()
        {
            descriptionText.text = "";
            gameObject.SetActive(false);
        }
        
    }
}