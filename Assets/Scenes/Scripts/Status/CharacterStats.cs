using System;
using System.Collections;
using UnityEngine;
using Enitity;

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

namespace Status
{
    public class CharacterStats : MonoBehaviour
    {
        private EnityFX fx;
        
        [Header("Major stats")]
        public Stats strength; // 1 point increase attackPower by 1 and crit.power by 1%
        public Stats agility; // 1 point increase evasion by 1% and crit.chance by 1%
        public Stats intelligence; // 1 point increase magic attackPower by 1 and magic resistance by 3
        public Stats vitality;// 1 point increase health by 3 or 5 points
    
        [Header("Offensive stats")]
        public Stats attackPower;
        public Stats critChance;
        public Stats critPower;          // default 150% of attackPower
    
        [Header("Defensive stats")]
        public Stats maxHealth;
        public Stats armor;
        public Stats evasion;
        public Stats magicResistance;
    
        [Header("Magic stats")]
        public Stats fireDamage;
        public Stats iceDamage;
        public Stats lightningDamage;
    
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

        protected virtual void Start()
        {
            critPower.SetBaseValue(150);
            currentHealth = maxHealth.GetValue();
            
            fx = GetComponentInChildren<EnityFX>();
        }

        protected void Update()
        {
            
        }
        
        private IEnumerator StatModCoroutine(int modifier, float duration, Stats stat)
        {
            stat.AddModifier(modifier);
            yield return new WaitForSeconds(duration);
            stat.RemoveModifier(modifier);
        }

        protected virtual void Die()
        {
            isDead = true;
        }
        
        public int GetMaxHealthValue() => maxHealth.GetValue() + vitality.GetValue() * 5;
    }
}