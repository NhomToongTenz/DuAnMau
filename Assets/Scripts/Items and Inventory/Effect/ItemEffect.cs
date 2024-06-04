using UnityEngine;

//[CreateAssetMenu(fileName = "New Item Effect", menuName = ("Inventory System/Items/Item Effect"))]
public class ItemEffect : ScriptableObject
{
    public virtual void ExecuteEffect(Transform _respawnPos)
    {
        Debug.Log("Item effect executed");
    }
    
}
