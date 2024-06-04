using Unity.VisualScripting;
using UnityEngine;
using Plane = System.Numerics.Plane;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();
    private CircleCollider2D circleCollider2D => GetComponent<CircleCollider2D>();
    
    private Player player;
    
    private bool canGrow;
    private float growSpeed = 5;
    
    private float crystalExistTimer;
    
    
    private bool canExplode;
    private bool canMoveToEnemy;
    private float crystalSpeed;
    
    private Transform closestTarget;
    [SerializeField] private LayerMask enemyLayer;
   
    
    public void SetupCrystal(float timeToExist, bool canExplode, bool canMoveToEnemy, float crystalSpeed, Transform _closestTarget, Player _player)
    {
        crystalExistTimer = timeToExist;
        this.canExplode = canExplode;
        this.canMoveToEnemy = canMoveToEnemy;
        this.crystalSpeed = crystalSpeed;
        closestTarget = _closestTarget;
        player = _player;
    }
   
    
    public void ChooseRandomEnemy()
    {
        float radius =SkillManager.SkillManager.instance.blackholeSkill.GetBlackholeRadius();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        //closestEnemy = enemies[Random.Range(0, enemies.Length)].transform;
        if(enemies.Length > 0)
            closestTarget = enemies[Random.Range(0, enemies.Length)].transform;
        
    }
    
    private void Update()
    {   
        crystalExistTimer -= Time.deltaTime;
        
        if (crystalExistTimer < 0)
        {
            FinishCrystal();
        }

        //flip crystal to enemy
        FlipCrystalToEnemy();
        
        if (canMoveToEnemy)
        {   
            if(closestTarget == null)
                return;
            //RotateCrystalToEnemy();
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, crystalSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, closestTarget.position) < 1)
            { 
                FinishCrystal();
                canMoveToEnemy = false;
            }
        }
        
        if(canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, growSpeed * Time.deltaTime);
    }
    
    
    //Flip and rotate crystal to enemy
    private void FlipCrystalToEnemy()
    {   
        if (closestTarget == null)
            return;
        Vector2 direction = closestTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    

    public void FinishCrystal()
    {
        if (canExplode)
        {
           // Debug.Log("Explode");
            canGrow = true;
            animator.SetTrigger("Explode");
        }
        else
        {
            SelfDestroy();
        }
    }

    private void AnimationExplodeEnd()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);
        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                player.stats.DoMagicDamage(hit.GetComponent<CharacterStats>());
                
                ItemData_Equipment equipmentAmulet = Inventory.instance.GetEquipmentType(EquipmentType.Amulet) as ItemData_Equipment;
                if (equipmentAmulet != null)
                {
                    equipmentAmulet.Effect(hit.transform);
                }
            }
        }
        
    }
    
    public void SelfDestroy() => Destroy(gameObject);
    
   
   
   
}
