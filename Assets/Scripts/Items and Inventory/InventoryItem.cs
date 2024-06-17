using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;
    
    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;    
        // TODO: Add stack size to item data
        AddStack();
    }
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
