using System;using UnityEngine;

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Inventory System/Items/Effects/BuffEffect")]
public class BuffEffect : ItemEffect
{
    private PlayerStats playerStats;
    [SerializeField] private StatType buffType;
    [SerializeField] private int buffAmount;
    [SerializeField] private float buffDuration;

    public override void ExecuteEffect(Transform _enemyPos)
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.IncreaStatBy(buffAmount, buffDuration, playerStats.GetStat(buffType));
    }
}
