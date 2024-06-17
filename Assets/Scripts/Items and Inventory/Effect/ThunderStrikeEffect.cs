using UnityEngine;

[CreateAssetMenu(fileName = "Thunder strike effect", menuName = "Inventory System/Items/Effects/Thunder strike effect")]
public class ThunderStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;

    public override void ExecuteEffect(Transform _respawnPos)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab, _respawnPos.position, Quaternion.identity);
        Destroy(newThunderStrike, 1f);
        
    }
}
