using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI_CraftList : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform craftSlotParent;
        [SerializeField] private GameObject craftSlotPrefab;

        [SerializeField] private List<ItemData_Equipment> craftEquipment; 

        private void Start()
        {
            // Get the craft equipment list from the parent object
            transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetupCraftList();
            // Setup the default craft window
            SetupDefaultCraftWindow();
        }


        public void SetupCraftList()
        {
            // Clear the current craft list
            for (int i = 0; i < craftSlotParent.childCount; i++)
            {
                Destroy(craftSlotParent.GetChild(i).gameObject);
            }
            
            // Loop through the craft equipment list and instantiate a new slot for each item
            for (int i = 0; i < craftEquipment.Count; i++)
            {
                GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
                newSlot.GetComponent<UI_CraftSlot>().SetupCraftSlot(craftEquipment[i]);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetupCraftList();
        }

        public void SetupDefaultCraftWindow()
        {   
            // If there is an item in the list, set the craft window to the first item in the list
            if(craftEquipment[0]!= null)
               GetComponentInParent<UI>().craftWindow.SetupCraftWindow(craftEquipment[0]);
        }
    }
}