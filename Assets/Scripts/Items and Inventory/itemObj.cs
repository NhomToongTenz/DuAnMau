using System;
using UnityEngine;

public class itemObj : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void SetupVisual()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }


    public void SetupItemObj(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisual();
    }


    public void PickupItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 7);   
            return;
        }


        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}