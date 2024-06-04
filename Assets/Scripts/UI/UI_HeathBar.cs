using System;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UI_HeathBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform healthBar;
    private Slider slider;
    
    private CharacterStats stats;
    
    
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        stats = GetComponentInParent<CharacterStats>();
        
               
        entity.onFlipped += FlipUI;
        stats.onHeathChanged += UpdateHeathUI;
    }

    void UpdateHeathUI()
    {
        slider.maxValue = stats.GetMaxHeathValue();
        slider.value = stats.currentHealth;
    }
    
    private void FlipUI() => healthBar.Rotate(0,180,0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        stats.onHeathChanged -= UpdateHeathUI;
    }
}
