using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;
    [Header("Level Details")]
    [SerializeField] private int level = 1;
  //  [SerializeField] private int experience = 0;
    
    [Range(0f, 1f)]
    [SerializeField] private float percantageModifier = 0.4f;
    
    protected override void Start()
    {
        ApplyLevelModify();

        base.Start();
        enemy = GetComponent<Enemy>();
        
        myDropSystem = GetComponent<ItemDrop>();
        
    }

    private void ApplyLevelModify()
    {   
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);
        
        Modify(attackPower);
        Modify(critChance);
        Modify(critPower);
        
        Modify(armor);
        Modify(maxHealth);
        Modify(evasion);
        Modify(magicResistance);
        
        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightningDamage);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
   
    }
    
    
    private void Modify(Stat _stat)
    {
        for (int i =  1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percantageModifier;
            
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    

    protected override void Die()
    {
        base.Die();
        enemy.Die();
        myDropSystem.GenerateDrop();
    }
}
