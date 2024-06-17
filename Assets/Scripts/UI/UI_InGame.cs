using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_InGame : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private Slider _healthSlider;

        [SerializeField] private Image dashCooldownImage;
        [SerializeField] private Image parryCooldownImage;
        [SerializeField] private Image crystalCooldownImage;
        [SerializeField] private Image swordCooldownImage;
        [SerializeField] private Image blackHoleCooldownImage;
        [SerializeField] private Image flaskCooldownImage;
        
        [SerializeField] private TextMeshProUGUI currentSoulsText;
        
        private SkillManager.SkillManager _skillManager;
        
        private void Start()
        {
            if(_playerStats != null)
                _playerStats.onHeathChanged += UpdateHealthSlider;
            _skillManager = SkillManager.SkillManager.instance;           
        }

        private void Update()
        {
            currentSoulsText.text = PlayerManager.instance.GetCurrency().ToString("#,#");
            
            if(Input.GetKeyDown(KeyCode.LeftShift) && _skillManager.dashSkill.dashUnlocked)
                SetCooldownOf(dashCooldownImage);
            
            if(Input.GetKeyDown(KeyCode.Q) && _skillManager.parrySkill.parryUnlocked)
                SetCooldownOf(parryCooldownImage);
            
            if(Input.GetKeyDown(KeyCode.F) && _skillManager.crystalSkill.crystalUnlocked)
                SetCooldownOf(crystalCooldownImage);
            
            if(Input.GetKeyDown(KeyCode.Mouse1) && _skillManager.swordSkill.swordUnlocked)
                SetCooldownOf(swordCooldownImage);
            
            if(Input.GetKeyDown(KeyCode.R) && _skillManager.blackholeSkill.blackholeUnlocked)
                SetCooldownOf(blackHoleCooldownImage);
            
            if(Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipmentType(EquipmentType.Flask) != null)
                SetCooldownOf(flaskCooldownImage);
            
            CheckCooldownOf(dashCooldownImage, _skillManager.dashSkill.coolDown);
            CheckCooldownOf(parryCooldownImage, _skillManager.parrySkill.coolDown);
            CheckCooldownOf(crystalCooldownImage, _skillManager.crystalSkill.coolDown);
            CheckCooldownOf(swordCooldownImage, _skillManager.swordSkill.coolDown);
            CheckCooldownOf(blackHoleCooldownImage, _skillManager.blackholeSkill.coolDown);
            
            CheckCooldownOf(flaskCooldownImage, Inventory.instance.flaskCooldown);
            
        }


        private void UpdateHealthSlider( )
        {
            _healthSlider.maxValue = _playerStats.GetMaxHeathValue();
            _healthSlider.value = _playerStats.currentHealth;
        }
        
        private void SetCooldownOf(Image image)
        {
            if(image.fillAmount <= 0)
                image.fillAmount = 1;
        }
        
        private void CheckCooldownOf(Image image, float cooldown)
        {
            if(image.fillAmount > 0)
            {
                image.fillAmount -= 1 / cooldown * Time.deltaTime;
            }
        }
    }
}