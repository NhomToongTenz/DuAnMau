using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory,
    Amulet,
    Flask,
}

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Items/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    
    [Header("Unique Item")]
    public float itemCooldown;
    public ItemEffect[] effects;
    [TextArea]
    public string itemEffectDescription;
    
    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;
    
    [Header("Offensive Stats")]
    public int attackPower;
    public int critChance;
    public int critDamage;
    
    [Header("Defensive Stats")]
    public int armor;
    public int health;
    public int evasion;
    public int magicResistance;
    
    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;

    // Crafting
    [Header("Craft Requirements")] 
    public List<InventoryItem> craftingMaterials;
    

    public void AddModifiers()
    {   
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        // Add the modifiers to the player stats
        // Major Stats
        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);
        
        // Offensive Stats
        playerStats.attackPower.AddModifier(attackPower);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critDamage);
        
        // Defensive Stats
        playerStats.armor.AddModifier(armor);
        playerStats.maxHealth.AddModifier(health);
        playerStats.evasion.AddModifier(evasion);
        
        // Magic Stats
        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightningDamage.AddModifier(lightningDamage);
        
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        // Remove the modifiers from the player stats
        // Major Stats
        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);
        
        // Offensive Stats
        playerStats.attackPower.RemoveModifier(attackPower);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critDamage);
        
        // Defensive Stats
        playerStats.armor.RemoveModifier(armor);
        playerStats.maxHealth.RemoveModifier(health);
        playerStats.evasion.RemoveModifier(evasion);
        
        // Magic Stats
        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightningDamage.RemoveModifier(lightningDamage);
        
    }
    
    public void Effect(Transform _enemyTransform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        foreach (ItemEffect effect in effects)
        {
            effect.ExecuteEffect(_enemyTransform);
        }
    }

    private int DescriptionLenght;
    public override string GetDescription()
    {
        sb.Length = 0;
        DescriptionLenght = 0;
        
        AddItemDes(strength, "Strength");
        AddItemDes(agility, "Agility");
        AddItemDes(intelligence, "Intelligence");
        AddItemDes(vitality, "Vitality");
        
        AddItemDes(attackPower, "Attack Power");
        AddItemDes(critChance, "Crit Chance");
        AddItemDes(critDamage, "Crit Damage");
        
        AddItemDes(armor, "Armor");
        AddItemDes(health, "Health");
        AddItemDes(evasion, "Evasion");
        AddItemDes(magicResistance, "Magic Resistance");
        
        AddItemDes(fireDamage, "Fire Damage");
        AddItemDes(iceDamage, "Ice Damage");
        AddItemDes(lightningDamage, "Lightning Damage");
        
        if(DescriptionLenght < 2)
            for (int i = 0; i < 2 - DescriptionLenght; i++)
            {
                sb.AppendLine();
                sb.Append(" ");
            }
        
        if(itemEffectDescription.Length > 0)
        {
            sb.AppendLine();
            sb.Append(itemEffectDescription);
        }
        
        return sb.ToString();
    }
    
    private void AddItemDes(int _value, string _name)
    {
        if (_value != 0)
        {
            if(sb.Length > 0)
                sb.AppendLine();
            if(_value > 0)
                sb.Append( "+ " + _value + " " + _name);
            
            DescriptionLenght++;
            
        }
    }
}
