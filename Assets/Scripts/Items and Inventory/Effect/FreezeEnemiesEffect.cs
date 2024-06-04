using UnityEngine;

[CreateAssetMenu(fileName = "Freeze enemies effect", menuName = "Inventory System/Items/Effects/FreezeEnemiesEffect")]
public class FreezeEnemiesEffect : ItemEffect
{
    [SerializeField] private float duration;


    public override void ExecuteEffect(Transform _transform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        if(playerStats.currentHealth > playerStats.GetMaxHeathValue() * .1f)
            return;
        
        if(!Inventory.instance.CanUseArmor())
            return;
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in enemies)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
        }
    }
}
