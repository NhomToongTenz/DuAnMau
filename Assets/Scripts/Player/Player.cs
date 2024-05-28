using System.Collections;
using System.Collections.Generic;
using Enitity;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    
    [Header("Move Info")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float miveSpeedDefault;
    private float jumpForceDefault;
    
    [Header("Dash Info")]
    public float dashTimeDuration = 0.1f;
    public float dashSpeed = 10f;
    private float dashSpeedDefault;
    public float dashDirection { get; private set; } = 1f;
    
    public PlayerInputHandler inputHandler { get; private set; }
    
    
    
    public bool isBusy { get; private set; }

    #region State

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    
    #endregion
    
    
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = gameObject.AddComponent<PlayerStateMachine>();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        //groundedState = new PlayerGroundedState(this, stateMachine, "grounded");
        airState = new PlayerAirState(this, stateMachine, "jump");
        dashState = new PlayerDashState(this, stateMachine, "dash");
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDash();
        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        inputHandler = GetComponent<PlayerInputHandler>();
        
        miveSpeedDefault = moveSpeed;
        jumpForceDefault = jumpForce;
        dashSpeedDefault = dashSpeed;
    }

    public void AnimationTriggers()=> stateMachine.currentState.AnimationFinishTrigger();
    public IEnumerator SetBusy(float duration)
    {
        isBusy = true;
        yield return new WaitForSeconds(duration);
        isBusy = false;
    }


    public override bool IsGroundDetected()
    {
        return base.IsGroundDetected();
    }
    
    private void CheckForDash()
    {
        
        if(IsTouchingWall())
            return;
        
        
        
        if (inputHandler.dashInput)
        {
            dashDirection = inputHandler.movementInput.x;
            if(dashDirection == 0)
                dashDirection = facingDirection;
            stateMachine.ChangeState(dashState);
            
            
        }
        
            
    }
    
}
