using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    
    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;
    
    [Header("Move Info")]
    public float moveSpeed = 3f;
    public float idleTime = 1f;
    public float battleTime;
    private float defaultMoveSpeed;
    
    [Header("Attack info")]
    public float atkDistance;
    public float atkCooldown;
    [HideInInspector] public float lastTimeAttacked;
    public string lastAnimBoolName{get; private set;}
    public EnemyStateMachine stateMachine { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        
        defaultMoveSpeed = moveSpeed;
    }
    
    protected override void Update()
    {
        base.Update();
        
        stateMachine.currentEnemyState.Update();
    }


    public override void SlowEntityBy(float slowPercentage, float duration)
    {
        base.SlowEntityBy(slowPercentage, duration);
        moveSpeed *= (1 - slowPercentage);
        anim.speed *= (1 - slowPercentage);
        
        Invoke("ReturnDefaultSpeed", duration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
    }

    public virtual void AssignLastAnimBoolName(string _animBoolName) => lastAnimBoolName = _animBoolName;
    
    public virtual void AnimationFinishTrigger() => stateMachine.currentEnemyState.AnimationFinishTrigger();

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }
    
    public virtual void FreezeTimeFor(float _time) => StartCoroutine(FreezeTimeCoroutine(_time));
    
    protected virtual IEnumerator FreezeTimeCoroutine(float _time)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_time);
        FreezeTime(false);
    }

    #region Counter Attack Win

    

   
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
            
        }
        return false;
    }
    #endregion
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 50, whatIsPlayer);
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + atkDistance * facingDirection, transform.position.y));
    }
    
}
