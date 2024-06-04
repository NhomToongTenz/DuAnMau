using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{   
    public static Inventory instance;
    
    public List<ItemData> startingEquipment;
    
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    
    public List<InventoryItem> stashItems;
    public Dictionary<ItemData, InventoryItem> stashDictionary;
    
    
    [Header("Inventory UI")]
    
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;
    
    
    [Header("Items cooldown")]
    private float lastFlaskUsed;
    private float lastArmorUsed;
    
    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentItemSlot;
    private UI_StatSlot[] statSlots;
    
    public float flaskCooldown { get; private set; } = 5f;
    private float armorCooldown = 5f;
    
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        
        stashItems = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = statSlotParent.GetComponentsInChildren<UI_StatSlot>();
        
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        for (int i = 0; i < startingEquipment.Count; i++)
        {   
            if(startingEquipment[i] != null)
                AddItem(startingEquipment[i]);
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            AddItem(oldEquipment);
        }


        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);

        UpdateSlotUI();
    }

    

    public void UnEquipItem(ItemData_Equipment itemToUnequip)
    {
        // If there is an item to unequip then remove it from the equipment list and dictionary
        if(equipmentDictionary.TryGetValue(itemToUnequip, out InventoryItem value))
        {   
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToUnequip);
            itemToUnequip.RemoveModifiers();
        }
    }

    private void UpdateSlotUI()
    {
        // Update the UI for the equipment slots
        for (int i = 0; i < equipmentItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentItemSlot[i].slotType)
                    equipmentItemSlot[i].UpdateSlot(item.Value);
            }
        }
        
        // Update the UI for the inventory slots
        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }
        
        // Update the UI for the stash slots
        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }
        
        
        // Update the UI for the inventory slots
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }
        
        
        // Update the UI for the stash slots
        for (int i = 0; i < stashItems.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stashItems[i]);
        }

        // update the UI for the stat slots
        UpdateStatsUI();
        
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStatValueUI();
        }
    }

    public void AddItem(ItemData _item)
    {
        if(_item.itemType == ItemType.Equipment && CanAddItem())
        {
            AddToInventory(_item);
        }
        else if(_item.itemType == ItemType.Material)
        {
            AddToStash(_item);
        }
        
        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if(stashDictionary.TryGetValue(_item, out InventoryItem stashItem))
        {
            stashItem.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stashItems.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item, out InventoryItem inventoryItem))
        {
            inventoryItem.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item, out InventoryItem inventoryItem))
        {
            if(inventoryItem.stackSize <= 1)
            {
                inventory.Remove(inventoryItem);
                inventoryDictionary.Remove(item);
            }
            else
            {
                inventoryItem.RemoveStack();
            }
        }
        
        if(stashDictionary.TryGetValue(item, out InventoryItem stashItem))
        {
            if(stashItem.stackSize <= 1)
            {
                stashItems.Remove(stashItem);
                stashDictionary.Remove(item);
            }
            else
            {
                stashItem.RemoveStack();
            }
        }
        
        UpdateSlotUI();
    }

    public bool CanAddItem()
    {
        if(inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        return true;
    }

    public bool CanCraft(ItemData_Equipment itemToCraft, List<InventoryItem> _requiredMaterials)
    {   
        List<InventoryItem> matialsToRemove = new List<InventoryItem>(); // List of materials to remove from the stash 
       for(int i = 0; i < _requiredMaterials.Count; i++) // Check if the player has the required materials to craft the item
       {
           // Check if the player has the required materials
           if(stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashItem))
           { // If the player has the required materials then check if the player has enough of the materials
              if(stashItem.stackSize < _requiredMaterials[i].stackSize)
              {
                  //Debug.Log("You do not have the required materials");
                  return false;
              }
              else
              {
                  //matialsToRemove.Add(stashItem);
                  matialsToRemove.Add(stashItem);
              }
           }
           else
           {
               Debug.Log("not enough materials");
               return false;
           }
       }
        
       // If the player has the required materials then remove the materials from the stash and add the crafted item to the inventory
       for (int i = 0; i < matialsToRemove.Count; i++)
       {
           RemoveItem(matialsToRemove[i].data);
       }
       // Add the crafted item to the inventory
       AddItem(itemToCraft);
       Debug.Log("Crafting Successful");
       return true;
       
    }
    
    public List<InventoryItem>  GetEquipmentList() => equipment;
    public List<InventoryItem> GetStashList() => stashItems;
    
    public ItemData_Equipment GetEquipmentType(EquipmentType _equipmentType)
    {
        ItemData_Equipment equipedItem = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType == _equipmentType)
                equipedItem = item.Key;
        }

        return equipedItem;
    }

    public void UseFlask()
    {
        ItemData_Equipment currentFlask = GetEquipmentType(EquipmentType.Flask);
        
        if(currentFlask == null)
        {
           // Debug.Log("No Flask Equipped");
            return;
        }
        
        bool canUseFlask = Time.time > lastFlaskUsed + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentFlask.itemCooldown;
            currentFlask.Effect(null);
            lastFlaskUsed = Time.time;
        }else
        {
            Debug.Log("Flask is on cooldown");
        }
        
    }

    public bool CanUseArmor()
    {
        ItemData_Equipment currentArmor = GetEquipmentType(EquipmentType.Armor);
        
        if(Time.time > lastArmorUsed + armorCooldown)
        {
            armorCooldown = currentArmor.itemCooldown;  
            lastArmorUsed = Time.time;
            return true;
        }
        Debug.Log("Armor is on cooldown");
        return false;
    }
}
