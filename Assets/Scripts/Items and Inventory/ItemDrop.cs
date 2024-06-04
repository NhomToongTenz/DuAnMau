using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{   
    [SerializeField] private int amountOfItemsToDrop;
    [SerializeField] private ItemData[] possibleItemsDrop;
    private List<ItemData> dropList = new List<ItemData>();
    
    [SerializeField] private GameObject itemObjPrefab;

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibleItemsDrop.Length; i++)
        {
            if(Random.Range(0, 100) <= possibleItemsDrop[i].dropChance)
            {
                dropList.Add(possibleItemsDrop[i]);
            }
        }
        
        for (int i = 0; i < amountOfItemsToDrop; i++)
        {
            if (dropList.Count > 0)
            {
                ItemData randomItem = dropList[Random.Range(0, dropList.Count )];
                dropList.Remove(randomItem);
                DropItem(randomItem);
            }
           
           
        }
        
    }
    
    protected void DropItem(ItemData item)
    {
        GameObject itemObj = Instantiate(itemObjPrefab, transform.position, Quaternion.identity);
        Vector2 velocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));
        itemObj.GetComponent<itemObj>().SetupItemObj(item, velocity);
        
    }
}
