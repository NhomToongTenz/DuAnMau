using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI.UI UI;

    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValueText;

    [TextArea] [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if (statNameText != null)
            statNameText.text = statName;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateStatValueUI();

        UI = GetComponentInParent<UI.UI>();
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();

            switch (statType)
            {
                case StatType.maxHealth:
                    statValueText.text = playerStats.GetMaxHeathValue().ToString();
                    break;
                case StatType.attackPower:
                    statValueText.text =
                        (playerStats.attackPower.GetValue() + playerStats.strength.GetValue()).ToString();
                    break;
                case StatType.critPower:
                    statValueText.text =
                        (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();
                    break;
                case StatType.critChance:
                    statValueText.text =
                        (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();
                    break;
                case StatType.evasion:
                    statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();
                    break;
                case StatType.magicResistance:
                    statValueText.text =
                        (playerStats.magicResistance.GetValue() + playerStats.intelligence.GetValue() * 3).ToString();
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.statToolTip.ShowToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.statToolTip.HideToolTip();
    }
}