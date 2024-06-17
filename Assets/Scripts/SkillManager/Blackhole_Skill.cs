using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Blackhole_Skill : Skill
{   
    [SerializeField] private UI_SkillTreeSLot blackholeUnlockBtn;
    public bool blackholeUnlocked { get; private set; }
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private float blackholeDuration;
    [Space]
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growFactor;
    [SerializeField] private float shrinkSpeed;
    
    Blackhole_Skill_Controller currentBlackhole;
    
    
    private void UnlockBlackhole()
    {
        if(blackholeUnlockBtn.unlocked)
        {
            blackholeUnlocked = true;
        }    
        
    }
    
    
    
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }
    
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject blackhole = Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);
        currentBlackhole = blackhole.GetComponent<Blackhole_Skill_Controller>();
        currentBlackhole.SetUpBlackHole(maxSize, growFactor, shrinkSpeed, amountOfAttacks, cloneAttackCooldown, blackholeDuration);
    }
    
    protected override void Start()
    {
        base.Start();
        
        blackholeUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockBlackhole);
    }
    
    protected override void Update()
    {
        base.Update();
    }
    public bool SkillCompleted()
    {
        if(!currentBlackhole)
            return false;
        
        
        if (currentBlackhole.playerCanExitBlackhole)
        {
            currentBlackhole = null;
            return true;
        }
        return false;
    }
    public float GetBlackholeRadius()
    {
        return maxSize/2;
    }
}
