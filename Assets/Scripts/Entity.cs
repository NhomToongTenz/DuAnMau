using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Component

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D csCol { get; private set; }
    #endregion
    
    [Header("Knockback Info")]  
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;
    
    
    
   // public Action OnFlipped;

    [Header("Collison Info")] public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    //[SerializeField] protected LayerMask whatIsCharacter;

    
    public System.Action onFlipped;


    [HideInInspector] public int facingDirection = 1; // 1 is right, -1 is left
    protected bool isFacingRight = true;

    protected virtual void Awake()
    {

    }
    
    protected virtual void Update()
    {
        
    }

    protected virtual void Start()
    {   
        sr = GetComponentInChildren<SpriteRenderer>();
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        csCol = GetComponent<CapsuleCollider2D>();
    }
    
    public virtual void DamageImpact() =>  StartCoroutine("HitKnockback");
    
    public virtual void SlowEntityBy(float slowPercentage, float duration)
    {
       // StartCoroutine(SlowEntity(slowPercentage, duration));
    }
    
    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }
    
    
    protected virtual IEnumerator HitKnockback()
    {
        
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity

    public void SetZeroVelocity()
    {
        if(isKnocked)
            return;
        rb.velocity = Vector2.zero;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
       if(isKnocked)
           return;
       
       rb.velocity = new Vector2(_xVelocity, _yVelocity);   
       FlipController(_xVelocity);
    }
    
    #endregion



    #region Collision

   // public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    //public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        
    // debug physics.raycast
    
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    
    //public virtual bool IsPlayerDetected() => Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsGround);
   
   
    
    
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance ,  wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        
    }

    #endregion
    
    
    
    
    
    
    //
    
    #region Flip
    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        
        if(onFlipped != null)
            onFlipped();

    }
    public virtual void FlipController(float xInput)
    {
        if(xInput > 0 && !isFacingRight)
            Flip();
        else if(xInput <0 && isFacingRight)
            Flip();
    }
    
    #endregion
    
    
    
    public virtual void Die()
    {
       // Destroy(gameObject);
    }

}