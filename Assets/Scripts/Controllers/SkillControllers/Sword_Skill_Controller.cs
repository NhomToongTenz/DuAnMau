using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{   
    
    [SerializeField] private float returnSpeed = 10f;
    private Animator animator;
    private Rigidbody2D rb;
    private Player player;
    private CircleCollider2D circleCollider2D;
    
    private bool canRotate = true;
    private bool isReturning = false;
    
    [Header("Pierce info")]
    [SerializeField] private float pierceForce;
    [SerializeField] private int pierceCount;
    
    
    [Header("Bounce info")]
    private float boundForce = 10f;
    private bool isBouncing ;
    private int amountOfBounces ;
    private List<Transform> enemyTarget;
    private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;
    
    private float freezeTimeDuration;
    
    private float hitTimer;
    private float hitCooldown ;
    
    private float spinDirection = 1;
    
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        
    }
    
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
   
    private void Update()
    {
        if(canRotate)
            transform.right = rb.velocity.normalized;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, player.transform.position) < 1f)
                player.CatchTheSword();
        }

        BounceLogic();

        SpinningLogic();
    }

    private void SpinningLogic()
    {
        if (isSpinning)
        {
            if(Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;
                
                // transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 2.4f * Time.deltaTime);

                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }
                
                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            SwordSkillDamage(hit.GetComponent<Enemy>());
                    }

                }
            }
        }
        
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, boundForce * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            {   
                SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
                //enemyTarget[targetIndex].GetComponent<Enemy>().DamageImpact();
                //enemyTarget[targetIndex].GetComponent<Enemy>().StartCoroutine("FreezeTimeCoroutine", freezeTimeDuration);
                
                
                targetIndex++;
                
                amountOfBounces--;

                if (amountOfBounces <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                
                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if(isReturning)
            return;


        if (other.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            SwordSkillDamage(enemy);
        }
        
        // other.GetComponent<Enemy>()?.DamageImpact();
        
        
        

        SetUpTargetForBounce(other);
        
        StuckInto(other);
    }

    private void SwordSkillDamage(Enemy enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        
        player.stats.DoDamge(enemy.GetComponent<CharacterStats>());
        
        if(player.skillManager.swordSkill.timeStopUnlocked)
            enemy.FreezeTimeFor(freezeTimeDuration);
        
        if(player.skillManager.swordSkill.volnurableUnlocked)
            enemyStats.MakeVulnerableFor(freezeTimeDuration);
        
        ItemData_Equipment equipmentAmulet = Inventory.instance.GetEquipmentType(EquipmentType.Amulet);
        
        if(equipmentAmulet != null)
        {
            equipmentAmulet.Effect(enemy.transform);
        }
        
    }

    private void SetUpTargetForBounce(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
                
            }
        }
    }

    public void SetUpSword(Vector2 _launchDir, float _gravityScale, Player _player, float _freezeTimeDuration, float _returnSpeed )
    {
        player = _player;
        rb.gravityScale = _gravityScale;
        rb.velocity = _launchDir;
        if(pierceCount <= 0)
            animator.SetBool("Rotation", true);
        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
        freezeTimeDuration = _freezeTimeDuration;
        Invoke("DestroyMe", 5f);
        
        //animator.SetBool("Rotation", true);
    }


    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
        
        // sword.skill.setcooldown
    }
    
    
    public void SetUpBounce(bool _isBouncing, int _amountOfBounces, float _boundForce)
    {   
        boundForce = _boundForce;
        isBouncing = _isBouncing;
        amountOfBounces = _amountOfBounces;
            //boundForce = _boundForce;
            enemyTarget = new List<Transform>();
    }
    
    public void SetUpPierce(int _pierceCount)
    {
        pierceCount = _pierceCount;
    }
    
        
    
    
    public void SetUpSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown = 0.5f)
    {
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        spinDuration = _spinDuration;
        hitCooldown = _hitCooldown;
    }
    
    
    
    

    private void StuckInto(Collider2D other)
    {
        if(pierceCount > 0 && other.GetComponent<Enemy>() != null)
        {
            pierceCount--;
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }
        
        canRotate = false;
        circleCollider2D.enabled = false;
        
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        if(isBouncing && enemyTarget.Count > 0)
            return;
        
        transform.parent = other.transform;
        animator.SetBool("Rotation", false);
    }
}
