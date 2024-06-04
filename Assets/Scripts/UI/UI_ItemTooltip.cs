using TMPro;
using UnityEngine;

namespace UI
{
    public class UI_ItemTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        
        [SerializeField] private int defaultFontSize = 32;
        
        
        public void ShowTooltip(ItemData_Equipment _itemData)
        {
            
            if(_itemData == null)
                return;
            
            itemNameText.text = _itemData.itemName;
            itemTypeText.text = _itemData.itemType.ToString();
            itemDescriptionText.text = _itemData.GetDescription();

            if (itemNameText.text.Length > 12)
                itemNameText.fontSize = defaultFontSize * .8f;
            else
                itemNameText.fontSize = defaultFontSize;
            
            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            //itemNameText.fontSize = defaultFontSize;
            gameObject.SetActive(false);
            
        }

    }
}