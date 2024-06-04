using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> hotKeys;
    
    public float maxSize;
    public float growFactor;
    public float shrinkSpeed;
    
    private bool canShrink;
    private bool canGrow = true;
    private float blackholeDuration;
    
    private bool canCreateHotKey = true;
    private int amountOfAttacks = 2 ;
    private float cloneAttackCooldown = 0.5f;
    private float cloneAttackTimer;
    private bool CloneAttackRelease;
    private bool playerCanDisapear = true;
    
    private List<GameObject> createdHotKeys = new List<GameObject>();
    public List<Transform> targets = new List<Transform>();
    
    public bool playerCanExitBlackhole{ get; private set; }
    
    public void SetUpBlackHole(float _maxSize, float _growFactor, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackholeDuration)
    {
        maxSize = _maxSize;
        growFactor = _growFactor;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackholeDuration = _blackholeDuration;
        
        if(SkillManager.SkillManager.instance.cloneSkill.useCrystalsInsteadOfClones)
           playerCanDisapear = false;
    }
   
    
    private void Update()
    {   
        
        cloneAttackTimer -= Time.deltaTime;
        blackholeDuration -= Time.deltaTime;

        if (blackholeDuration < 0)
        {
            blackholeDuration = Mathf.Infinity;
            if(targets.Count <0)
                ReleaseCloneAttack();
            else
                FinishBlackHoleAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }
        
        CloneAttackLogic();
        
        
        if (canGrow && !canShrink)
        { 
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growFactor * Time.deltaTime);
        }
        
        if (canShrink)
        {
            transform.localScale =Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
             if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        
        
    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;
        
        DestroyHotKeys();
        CloneAttackRelease = true;
        canCreateHotKey = false;

        if (playerCanDisapear)
        {
            playerCanDisapear = false;
             PlayerManager.instance.player.fx.MakeTransparent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if(cloneAttackTimer < 0 && CloneAttackRelease && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count); // Random target
            float xOffSet;
            if (Random.Range(0, 100) > 50)
                xOffSet = 2;
            else
                xOffSet = -2;

            if (SkillManager.SkillManager.instance.cloneSkill.useCrystalsInsteadOfClones)
            {
                SkillManager.SkillManager.instance.crystalSkill.CreateCrystal();
                SkillManager.SkillManager.instance.crystalSkill.CurrentCrystalChooseRandomTarget();
            }else 
                SkillManager.SkillManager.instance.cloneSkill.CreateClone(targets[randomIndex], new Vector3(xOffSet,0));// Create clone
            
            amountOfAttacks--;
            
            
            if (amountOfAttacks <= 0)
            {
               Invoke("FinishBlackHoleAbility", 1f);
            }
        }
    }

    private void FinishBlackHoleAbility()
    {   
        DestroyHotKeys();
        playerCanExitBlackhole = true;
        canShrink = true;
        CloneAttackRelease = false;
        
        
    }


    private void DestroyHotKeys()
    {
        if(createdHotKeys.Count <= 0)
            return;
        for (int i = 0; i < createdHotKeys.Count; i++)
        {
            Destroy(createdHotKeys[i]);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(other);
        }
    }

    private void CreateHotKey(Collider2D other)
    {   
        if(hotKeys.Count == 0)
            return;
        
        if(!canCreateHotKey)
            return;
        
        GameObject hotKey = Instantiate(hotKeyPrefab, other.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKeys.Add(hotKey);
        
        
        KeyCode randomKey = hotKeys[UnityEngine.Random.Range(0, hotKeys.Count)];
        hotKeys.Remove(randomKey);
            
        BlackHoleHotKeyController newHotKey = hotKey.GetComponent<BlackHoleHotKeyController>();
            
        newHotKey.SetHotKey(randomKey, other.transform, this);
    }

    private void OnTriggerExit2D(Collider2D other) => other.GetComponent<Enemy>()?.FreezeTime(false);
    

    // Add target to the list
    public void AddTarget(Transform target)
    {
        targets.Add(target);
    }
}
