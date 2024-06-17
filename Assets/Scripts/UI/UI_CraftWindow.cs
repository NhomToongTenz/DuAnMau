using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class UI_CraftWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button craftButton;
        
        [SerializeField] private Image[] materialIcons;
        
        public void SetupCraftWindow(ItemData_Equipment _item)
        {   
            
            craftButton.onClick.RemoveAllListeners(); // Remove all listeners
            // Clear the material icons
            for (int i = 0; i < materialIcons.Length; i++)
            {
                materialIcons[i].color = Color.clear;
                materialIcons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
            }
            // Loop through the crafting materials
            for(int i = 0; i < _item.craftingMaterials.Count; i++)
            {
                if(_item.craftingMaterials.Count > materialIcons.Length)
                    Debug.LogWarning("Too many materials for the craft window");
                
                materialIcons[i].sprite = _item.craftingMaterials[i].data.itemIcon;
                materialIcons[i].color = Color.white;
                
                TextMeshProUGUI materialSlotText = materialIcons[i].GetComponentInChildren<TextMeshProUGUI>();
                
                materialSlotText.text = _item.craftingMaterials[i].stackSize.ToString();
                materialSlotText.color = Color.white;
            }
            
            itemIcon.sprite = _item.itemIcon;
            itemName.text = _item.itemName;
            itemDescription.text = _item.GetDescription();
            
            craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(_item, _item.craftingMaterials)); // Add a new listenerœœœ
            
        }
        
    }
}