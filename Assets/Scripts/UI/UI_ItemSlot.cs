using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace UI
{

    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI itemText;

        public InventoryItem item;
        protected UI ui;

        protected virtual void Start()
        {
            ui = GetComponentInParent<UI>();
        }


        // Clear the slot 
        public void CleanUpSlot()
        {
            item = null;
            itemImage.color = Color.clear;
            itemImage.sprite = null;
            itemText.text = "";
        }

        public void UpdateSlot(InventoryItem _newItem)
        {
            item = _newItem;

            itemImage.color = Color.white;

            if (item != null)
            {
                itemImage.sprite = item.data.itemIcon;
                if (item.stackSize > 1)
                {
                    itemText.text = item.stackSize.ToString();
                }
                else
                {
                    itemText.text = "";
                }

            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {

            //Debug.Log("Equipped " + item.data.itemName);

            if (item == null)
                return;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Debug.Log("Removed " + item.data.itemName);
                Inventory.instance.RemoveItem(item.data);
                return;
            }

            if (item.data.itemType == ItemType.Equipment)
            {
                Inventory.instance.EquipItem(item.data);
//            Debug.Log("Equipped " + item.data.itemName);
            }
        
            ui.itemTooltip.HideTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(item == null)
                return;
            ui.itemTooltip.HideTooltip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(item == null)
                return;
            
            Vector2 mousePosition = Input.mousePosition;

            float xOffset = 0;
            float yOffset = 0;
            
            if(mousePosition.x > 600)
                xOffset = -250;
            else
                xOffset = 250;
            
            if (mousePosition.y > 320)
                yOffset = -150;
            else
                yOffset = 150;
            
            ui.itemTooltip.ShowTooltip(item.data as ItemData_Equipment);
            ui.itemTooltip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
        }
        
        
    }
}