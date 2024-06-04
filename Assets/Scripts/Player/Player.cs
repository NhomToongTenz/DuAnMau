using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    
    public bool isBusy { get; private set; }
    
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    private float moveSpeedDefault;
    private float jumpForceDefault;
    
    [Header("Dash Info")]
    public float dashSpeed = 13f;
    public float dashDuration = 0.2f;
    public float dashDirection { get; private set; }
    public float swordReturnImpact = 10f;
    private float dashSpeedDefault;
    
    
    
    
    public SkillManager.SkillManager skillManager { get; private set; }
    public GameObject sword {get; private set;}
    
    #region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState inAirState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlide wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackStateState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion
    
    protected override void Awake()
    {   
        base.Awake();
        stateMachine = gameObject.AddComponent<PlayerStateMachine>();  // stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        inAirState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlide(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        PrimaryAttackStateState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        
        //sword statement
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        //blackhole statement
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Jump");
        
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        skillManager = SkillManager.SkillManager.instance;
        stateMachine.Initialize(idleState);
        
        moveSpeedDefault = moveSpeed;
        jumpForceDefault = jumpForce;
        dashSpeedDefault = dashSpeed;
        
    }


    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState.Update();
        CheckForDashInput();
        //Debug.Log("Wall Detected: " + IsWallDetected());    
        //Debug.Log("Facing Direction: " + facingDirection);
        if(Input.GetKeyDown(KeyCode.F) && skillManager.crystalSkill.crystalUnlocked)
            skillManager.crystalSkill.CanUseSkill();
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.instance.UseFlask();
        
    }


    public override void SlowEntityBy(float slowPercentage, float duration)
    {
        base.SlowEntityBy(slowPercentage, duration);
        moveSpeed = moveSpeedDefault * (1 - slowPercentage);
        jumpForce = jumpForceDefault * (1 - slowPercentage);
        dashSpeed = dashSpeedDefault * (1 - slowPercentage);
        anim.speed *= (1 - slowPercentage);
        
        Invoke("ReturnDefaultSpeed", duration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = moveSpeedDefault;
        jumpForce = jumpForceDefault;
        dashSpeed = dashSpeedDefault;
        
    }


    public void AssignSword(GameObject _sword)
    {
        sword = _sword;
    }
    
    public void CatchTheSword()
    {   
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }
    
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    
   

    public void AnimationTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();


    private void CheckForDashInput()
    {
        if(IsWallDetected())
            return;
        
        if(skillManager.dashSkill.dashUnlocked == false)
            return;
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.SkillManager.instance.dashSkill.CanUseSkill())
        {
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0)
                dashDirection = facingDirection;
            stateMachine.ChangeState(dashState);    

        }
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
   
}
