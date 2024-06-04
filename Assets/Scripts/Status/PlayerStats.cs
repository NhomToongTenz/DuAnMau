using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreasHealthBy(int damage)
    {
        base.DecreasHealthBy(damage);

        if (Inventory.instance.GetEquipmentType(EquipmentType.Armor) == null)
            return;
        
        
        ItemData_Equipment curArmor = Inventory.instance.GetEquipmentType(EquipmentType.Armor);
        
        if(curArmor != null)
            curArmor.Effect(player.transform);
        
    }

    public override void OnEvasion()
    {
        base.OnEvasion();
        player.skillManager.dodgeSkill.CreateMirageOnDoDodge();
    }
    
    public void CloneDoDamge(CharacterStats targetStats, float multiplier)
    {
        if(TargetCanAvoidAttack(targetStats))
            return;
        
        int totalDamage = attackPower.GetValue() + strength.GetValue();
        
        if(multiplier > 0)
            totalDamage = (int) (totalDamage * multiplier);
        if(CanCrit())
            totalDamage = CalculateCritDamage(totalDamage);
        
        totalDamage = CheckTargetArmor(targetStats, totalDamage);
        targetStats.TakeDamage(totalDamage);
        
        DoMagicDamage(targetStats);
    }
    
    
}
