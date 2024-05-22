using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Componets

    public Animator anim{get; private set;}
    public Rigidbody2D rb{get; private set;}
    public SpriteRenderer sr{get; private set;}
    public CapsuleCollider2D col{get; private set;}

    #endregion
    
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask wallLayer;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected LayerMask groundLayer;
    
    public System.Action onFlipped;
    
    [HideInInspector] public int facingDirection = 1; // 1 is right, -1 is left
    protected bool isFacingRight => facingDirection == 1;
    
    protected virtual void Awake()
    {
       
    }
    
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    #region Velocity    
    
    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    
    #endregion
    
    #region Flip

    public virtual void FlipController(float xVelocity)
    {
        if(xVelocity > 0 && !isFacingRight || xVelocity < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        sr.flipX = !sr.flipX;
        onFlipped?.Invoke();
    }

    #endregion

    #region collision

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public virtual bool IsTouchingWall() => Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer);

    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance ,  wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        
    }

    #endregion
}
