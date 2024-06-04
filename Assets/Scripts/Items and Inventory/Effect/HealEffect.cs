using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Inventory System/Items/Effects/HealEffect")]
public class HealEffect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;
    public override void ExecuteEffect(Transform _enemyPos)
    {
        
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHeathValue() * healPercent );
        
       playerStats.IncreaseHealthBy(healAmount);
        
        
    }
}
