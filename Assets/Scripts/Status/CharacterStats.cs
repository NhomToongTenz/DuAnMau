using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum StatType
{
    strength,
    agility,
    intelegence,
    vitality,
    attackPower,
    critChance,
    critPower,
    armor,
    maxHealth,
    evasion,
    fireDamage,
    iceDamage,
    lightningDamage,
    magicResistance,
}


public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;
    
    [FormerlySerializedAs("strenght")] [Header("Major stats")]
    public Stat strength; // 1 point increase attackPower by 1 and crit.power by 1%
    public Stat agility; // 1 point increase evasion by 1% and crit.chance by 1%
    [FormerlySerializedAs("intelegence")] public Stat intelligence; // 1 point increase magic attackPower by 1 and magic resistance by 3
    public Stat vitality;// 1 point increase health by 5 points
    
    [FormerlySerializedAs("damage")] [Header("Offensive stats")]
    public Stat attackPower;
    public Stat critChance;
    public Stat critPower;          // default 150% of attackPower
    
    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;
    
    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
    
    public bool isIgnited; // does attackPower over time
    public bool isChiiled; // reduce movement speed by 20%
    public bool isShocked; // increase attackPower taken by 20%

    [SerializeField] private float ailmentDuration = 2;
    private float igniteTimer;
    private float chillTimer;
    private float shockTimer;
    
    
    
    private float igniteDamageCooldown =.3f;
    private float igniteDamageTimer;
    private int igniteDamage ;
    [SerializeField] private GameObject thunderStrikePrefab;
    private int shockDamage ;
    
    
    
    //ublic Stat evasion;
    public int currentHealth;
    
    public System.Action onHeathChanged;
    public bool isDead {get; private set;}
    private bool isVulnerable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {   
        critPower.SetBaseValue(150);
        currentHealth = GetMaxHeathValue();
        
        fx = GetComponent<EntityFX>();

    }
    
    protected virtual void Update()
    {
        igniteTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;
        
        igniteDamageTimer -= Time.deltaTime;
        
        
        if(igniteTimer < 0)
            isIgnited = false;
        
        if(chillTimer < 0)
            isChiiled = false;
        
        if(shockTimer < 0)
            isShocked = false;
        
        if(isIgnited)
            ApplyIgniteDamage();
        
    }
    
    public void MakeVulnerableFor(float _duration)
    {
        // if(isVulnerable)
        //     return;
        
        StartCoroutine(VulnerableForCoroutine(_duration));
    }
    
    private IEnumerator VulnerableForCoroutine(float _duration)
    {
        isVulnerable = true;
        yield return new WaitForSeconds(_duration);
        isVulnerable = false;
    }


    public virtual void IncreaStatBy(int _modifier, float _duration, Stat _statModify)
    { 
        StartCoroutine(StatModCoroutine(_modifier, _duration, _statModify));
        
    }

    private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statModify)
    {
        _statModify.AddModifier(_modifier);
        yield return new WaitForSeconds(_duration);
        _statModify.RemoveModifier(_modifier);
    }

    public virtual void DoDamge(CharacterStats target)
   {
       if (TargetCanAvoidAttack(target)) 
           return;
       
       int totalDamage = attackPower.GetValue() + strength.GetValue();

       if (CanCrit())
       {
           totalDamage = CalculateCritDamage(totalDamage);
          // Debug.Log("Crit attackPower: " + totalDamage);
       }
       
       totalDamage = CheckTargetArmor(target, totalDamage);

       target.TakeDamage(totalDamage);
       
       // if inventory current weapon is fire
       
        DoMagicDamage(target); // remove this line if u dont want to apply magic hit on primary attack
       //target.TakeDamage(attackPower.GetValue());
   }

   public virtual void DoMagicDamage(CharacterStats target)
   {
       int _fireDamage = fireDamage.GetValue();
       int _iceDamage = iceDamage.GetValue();
       int _lightningDamage = lightningDamage.GetValue();
       
       int totalMagicDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();
       
       totalMagicDamage = CheckTargetResitane(target, totalMagicDamage);

       target.TakeDamage(totalMagicDamage);
       
       if(Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0)
           return;
       
       
       AttemptyToApplyAilements(target, _fireDamage, _iceDamage, _lightningDamage);
   }

   #region Magiccal attackPower and ailements
   private void ApplyIgniteDamage()
   {
       if(igniteDamageTimer < 0)
       {
           igniteDamageTimer = igniteDamageCooldown;
          // Debug.Log("Burn: " + igniteDamage);
            
           DecreasHealthBy(igniteDamage);
           if(currentHealth <= 0 &&!isDead)
               Die();
       }
   }
   private static void AttemptyToApplyAilements(CharacterStats target, int _fireDamage, int _iceDamage,
       int _lightningDamage)
   {
       bool canIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
       bool canChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
       bool canShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;
       

       while (!canIgnite && !canChill && !canShock)
       {
           if(Random.value < 0.3f && _fireDamage > 0)
           {
               canIgnite = true;
               target.ApplyAilment(canIgnite, canChill, canShock);
               Debug.Log("Ignited"); 
               return;
           }
           if(Random.value < 0.5f && _iceDamage > 0)
           {
               canChill = true;
               target.ApplyAilment(canIgnite, canChill, canShock);
               Debug.Log("Chilled");
               return;
           }
           if(Random.value < 0.5f && _lightningDamage > 0)
           {
               canShock = true;
               target.ApplyAilment(canIgnite, canChill, canShock); 
               Debug.Log("Shocked");
               return;
           }
           
           
       }
       
       if(canIgnite)
           target.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.2f));
       
       if(canShock)
           target.SetupShockDamage(Mathf.RoundToInt(_lightningDamage * 0.1f));
       
       
       target.ApplyAilment(canIgnite, canChill, canShock);
   }
   
   public void ApplyAilment(bool _isIgnited, bool _isChilled, bool _isShocked)
   {
       bool canApplyIgnite = !isIgnited && !isChiiled && !isShocked;
       bool canApplyChill = !isIgnited && !isChiiled && !isShocked;
       bool canApplyShock = !isIgnited && !isChiiled;
       

       if (_isIgnited && canApplyIgnite)
       {
           isIgnited = _isIgnited;
           igniteTimer = ailmentDuration;
           
           fx.IgniteFxFor(ailmentDuration);
       }
       
       if (_isChilled && canApplyChill) {
           
             
             chillTimer = ailmentDuration;
             isChiiled = _isChilled; 
             
             float slowPercentage = 0.2f;
             GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentDuration);
             fx.ChillFxFor(ailmentDuration);
       }

       if (_isShocked && canApplyShock)
       {
           if (!isShocked)
           {
               ApplyShock(_isShocked);
               //Debug.Log("Shocked");
           }
           else
           {
               if(GetComponent<Player>() != null)
                   return;
               //Debug.Log("Thunder strike");
               HitNearestTargetWithShockStrike();
           }
           
       }
       
   }

   public void ApplyShock(bool _isShocked)
   {
       if(isShocked)
              return;
       shockTimer = ailmentDuration;
       isShocked = _isShocked;

       fx.ShockFxFor(ailmentDuration);
   }

   private void HitNearestTargetWithShockStrike()
   {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
       float closestDistance = Mathf.Infinity;
       Transform closestEnemy = null;
               
       foreach (var hit in colliders)
       {
           if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
           {
               float distance = Vector2.Distance(transform.position, hit.transform.position);
                        
               if(distance < closestDistance)
               {
                   closestDistance = distance;
                   closestEnemy = hit.transform;
               }
           }
                    
           if(closestEnemy == null) // if there is no enemy in the range of 25 units  
               closestEnemy = transform;
       }
               
       if (closestEnemy != null)
       {   
           //Debug.Log("Thunder strike");
                    
           GameObject thunderStrike = Instantiate(thunderStrikePrefab, transform.position, Quaternion.identity);
           thunderStrike.GetComponent<ShockStrikeController>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
       }
   }

   public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;
   public void SetupShockDamage(int _damage) => shockDamage = _damage;

    #endregion
    
   public virtual void TakeDamage(int damage)
    {
        DecreasHealthBy(damage);
        
        //currentHealth -= attackPower;
        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        
    }

    public virtual void IncreaseHealthBy(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > GetMaxHeathValue())
            currentHealth = GetMaxHeathValue();
        
        if(onHeathChanged != null)
            onHeathChanged();
    }
   
    protected virtual void DecreasHealthBy(int damage)
    {   
        if(isVulnerable)
            damage = Mathf.RoundToInt(damage * 1.1f);
        
        currentHealth -= damage;
        if(onHeathChanged != null)
            onHeathChanged();
    }
   
    protected virtual void Die()
    {
        isDead = true;
        //Destroy(gameObject);
    }

    #region Stat calculation
    protected int CheckTargetArmor(CharacterStats target, int totalDamage)
    {
        if(target.isChiiled)
            totalDamage -= Mathf.RoundToInt(target.armor.GetValue() * 0.8f);
        else
            totalDamage -= target.armor.GetValue() ;
        
        totalDamage -= target.armor.GetValue() ;
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    
    public virtual void OnEvasion()
    {
       
    }
    
    protected bool TargetCanAvoidAttack(CharacterStats target)
   {
       int totalEvasion = target.evasion.GetValue() + target.agility.GetValue();
       
       if(isShocked)
           totalEvasion += 20;
       
       if(Random.Range(0, 100) <totalEvasion)
       {
              target.OnEvasion();
           return true;
       }

       return false;
   }

    protected bool CanCrit()
   {
       int totalCritChance = critChance.GetValue() + agility.GetValue();
       
       if(Random.Range(0, 100) <= totalCritChance)
       {
           return true;
       }
       return false;
   }
   protected int CalculateCritDamage(int totalDamage)
   {
       float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;
      // Debug.Log("Crit power: " + totalCritPower);
       float critDamage = totalDamage * totalCritPower;
      // Debug.Log("Crit attackPower: " + critDamage);
       
       return Mathf.RoundToInt(critDamage);
   }
   
   private int CheckTargetResitane(CharacterStats target, int totalMagicDamage)
   {
       totalMagicDamage -= target.magicResistance.GetValue() + (target.intelligence.GetValue() * 3);
       totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
       return totalMagicDamage;
   }

   
   #endregion
   
   
   public Stat GetStat(StatType _statsType)
   {
       switch (_statsType)
       {
           case StatType.strength:
               return strength;
           case StatType.agility:
               return agility;
           case StatType.intelegence:
               return intelligence;
           case StatType.vitality:
               return vitality;
           case StatType.attackPower:
               return attackPower;
           case StatType.critChance:
               return critChance;
           case StatType.critPower:
               return critPower;
           case StatType.armor:
               return armor;
           case StatType.maxHealth:
               return maxHealth;
           case StatType.evasion:
               return evasion;
           case StatType.fireDamage:
               return fireDamage;
           case StatType.iceDamage:
               return iceDamage;
           case StatType.lightningDamage:
               return lightningDamage;
           case StatType.magicResistance:
               return magicResistance;
           default:
               return null;
       }
   }
   
   public int GetMaxHeathValue() =>  maxHealth.GetValue()  + vitality.GetValue() * 5;
   
   
   
}
