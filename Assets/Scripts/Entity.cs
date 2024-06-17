using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Entity : MonoBehaviour
    {
        #region Components

        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public SpriteRenderer sr { get; private set; }
        // publick CharacterStats stats { get; private set; }
        public CapsuleCollider2D cd { get; private set; }
        #endregion

        [Header("Knockback info")]
        [SerializeField] protected Vector2 knockbackPower = new Vector2(7, 12);
        [SerializeField] protected Vector2 knockOffset = new Vector2(0.5f, 2f);
        [SerializeField] protected float knockDuration = 0.3f;
        protected bool isKnockback;

        [Header("Collision info")]
        public Transform attackCheck;
        public float attackCheckRadius = 1.2f;
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected float groundCheckDistance = 1f;
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected float wallCheckDistance = 0.8f;
        [SerializeField] protected LayerMask whatIsGround;

        public int knockDirection { get; private set; }
        public int facingDirection { get; private set; } = 1;
        protected bool facingRight = true;

        public System.Action onFlipped;

        protected void Awake()
        {
        }

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            // stats = GetComponent<CharacterStats>();
            cd = GetComponent<CapsuleCollider2D>();
        }

        protected virtual void Update()
        {

        }

        public virtual void SlowEntity(float _slowPercent, float _duration)
        {
        }

        protected virtual void ReturnDefaultSpeed()
        {
            anim.speed = 1;
        }

        public virtual void DamageImpact() => StartCoroutine("HitKnockback");

        public virtual void SetupKnockDir(Transform _damgeDir)
        {
            if(_damgeDir.position.x < transform.position.x)
                knockDirection = 1;
            else
                knockDirection = -1;
        }

        public void SetupKnockbackPower(Vector2 _knockPower) => _knockPower = knockbackPower;

        protected virtual IEnumerator HitKnockback()
        {
            isKnockback = true;

            float xOffset = Random.Range(knockOffset.x, knockOffset.y);

            if(knockbackPower.x > 0 || knockbackPower.y > 0)
                rb.velocity = new Vector2((knockbackPower.x + xOffset) * knockDirection, knockbackPower.y);

            yield return new WaitForSeconds(knockDuration);

            isKnockback = false;
            SetupZeroKnockbackPower();
        }

        protected virtual void SetupZeroKnockbackPower()
        {
        }

        #region Velocity

        public void SetZeroVelocity()
        {
            if(isKnockback)
                return;
            rb.velocity = Vector2.zero;
        }

        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            if(isKnockback)
                return;
            rb.velocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);

        }
        #endregion

        #region Collision

        public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        }

        #endregion
        #region Flip

        public virtual void Flip()
        {
            facingRight = !facingRight;
            facingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);

            if (onFlipped != null)
                onFlipped();
        }

        public virtual void FlipController(float x)
        {
            if(x > 0 && !facingRight || x < 0 && facingRight)
                Flip();
        }

        #endregion

        public virtual void Die()
        {

        }

    }
}