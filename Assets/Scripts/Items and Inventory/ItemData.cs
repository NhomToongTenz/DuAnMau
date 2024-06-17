using System.Text;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory System/Items/Item Data")]
public class ItemData : ScriptableObject
{   
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    [Range(0,100)]
    public float dropChance;
    
    public int itemID;
    
    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}
