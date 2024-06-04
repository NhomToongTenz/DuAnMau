using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
   
   public class UI_CraftSlot : UI_ItemSlot
   {
      protected override void Start()
      {
         base.Start();
         
         
      }

      public void SetupCraftSlot(ItemData_Equipment _item)
      {
         
         if(_item == null)
            return;
         
         item.data = _item;
         
         itemImage.sprite = _item.itemIcon;
         itemText.text = _item.itemName;

         if (itemText.text.Length > 12)
            itemText.fontSize = itemText.fontSize * 0.7f;
         else
            itemText.fontSize = 24;

      }
      
      public override void OnPointerDown(PointerEventData eventData)
      {
         ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
      }
   }
}