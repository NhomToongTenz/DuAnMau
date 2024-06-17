using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    private Animator aim;
    [SerializeField] private float colorLoosingSpeed = 0.1f;
    
    private Player player;
    
    private float CloneTimer = 0;
    private float attackMultiplier;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    private bool _canDuplicateClone;
    public int facingDirection { get; private set; } = 1;
    private float chanceToDuplicate;
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        aim = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CloneTimer -= Time.deltaTime;

        if (CloneTimer < 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLoosingSpeed));
            if(spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }    
    }

    public void SetupClone(Transform _newTrans, float _cloneDuration, bool _canAttack, Vector3 _offset , Transform _closestEnemy, bool _canDuplicateClone, float chanceToDuplicate, Player _player, float _attackMultiplier)
    {
        player = _player;
        attackMultiplier = _attackMultiplier;
        if(_canAttack)
            aim.SetInteger("AttackNumber", Random.Range(1,3));
        transform.position = _newTrans.position + _offset;
        CloneTimer = _cloneDuration;
        
        closestEnemy = _closestEnemy;
        FaceClosestTarget();
        this._canDuplicateClone = _canDuplicateClone;
        this.chanceToDuplicate = chanceToDuplicate;
    }
    
    private void FaceClosestTarget()
    {
        
        
        if(closestEnemy != null)
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDirection = -1;
                transform.Rotate(0, 180, 0);
            }
    }
    
    
    private void AttackTriggers()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {   
                //player.stats.DoDamge(enemy.GetComponent<CharacterStats>());
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                
                playerStats.CloneDoDamge(enemyStats, attackMultiplier);
                if (player.skillManager.cloneSkill.canApplyOnHitEffect)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipmentType(EquipmentType.Weapon);
                    if (weaponData != null)
                    {
                        weaponData.Effect(enemy.transform);
                    }
                }    
                
                if (_canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.SkillManager.instance.cloneSkill.CreateClone(enemy.transform, new Vector3(.5f*facingDirection, 0, 0));
                        
                    }
                }
            }
        }
    }
    
    private void AnimationTrigger()
    {
        CloneTimer = -0.1f;
    }
    
    
    
}
