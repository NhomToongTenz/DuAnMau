using System;
using UnityEngine;
using UnityEngine.UI;
using Enitity;
using Status;

namespace UI
{
    public class UI_HealthBar : MonoBehaviour
    {
        private Entity entity;
        private RectTransform healthBar;
        private Slider slider;
        
        private CharacterStats stats;
        private void Start()
        {
            healthBar = GetComponent<RectTransform>();
            entity = GetComponentInParent<Entity>();
            slider = GetComponentInChildren<Slider>();
            
            stats = GetComponentInParent<CharacterStats>();
            
           // entity.onFlipped += FlipUI;
            //stats.onHeathChanged += UpdateHealthUI;
        }

        void UpdateHealthUI()
        {
           slider.maxValue = stats.GetMaxHealthValue(); 
           slider.value = stats.currentHealth;
        }
        
        private void FlipUI() => healthBar.Rotate(0,180,0);

        private void OnDisable()
        {
            //entity.onFlipped -= FlipUI;
            //stats.onHealthChanged -= UpdateHealthUI;
        }
    }
}