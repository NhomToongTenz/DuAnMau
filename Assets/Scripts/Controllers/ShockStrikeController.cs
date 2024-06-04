using UnityEngine;

public class ShockStrikeController : MonoBehaviour
{
    [SerializeField] private CharacterStats targetStats;
    [SerializeField] private float speed;
    private int damage;
    
    private Animator animator;
    private bool isHit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

    }

    public void Setup(int _damge, CharacterStats _targetStats)
    {
        damage = _damge;
        targetStats = _targetStats;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(!targetStats)
            return;
        
        if(isHit)
            return;
        
        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;
        
        
        if(Vector2.Distance(transform.position, targetStats.transform.position) < 0.1f)
        {
            animator.transform.localRotation = Quaternion.identity;
            animator.transform.localPosition = new Vector3(0, .5f);
            
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(2, 2);
           
            
            
           Invoke("DamageAndSelfDestroy", 0.2f);
           isHit = true;
           animator.SetTrigger("Hit");
           
        }
    }
    
    private void DamageAndSelfDestroy()
    {   
        targetStats.ApplyShock(true);
        targetStats.TakeDamage(1);
        Destroy(gameObject, .4f);
    }
    
    
}
