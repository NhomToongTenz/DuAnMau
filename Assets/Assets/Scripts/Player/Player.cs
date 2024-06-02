using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    
    [Header("Move Info")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
   [HideInInspector] public Vector2 movementInput;
    
    public bool isBusy { get; private set; }

    #region State

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
    public PlayerAirState airState { get; private set; }

    #endregion
    
    
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = gameObject.AddComponent<PlayerStateMachine>();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        groundedState = new PlayerGroundedState(this, stateMachine, "grounded");
        airState = new PlayerAirState(this, stateMachine, "air");
        
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void AnimationTriggers()=> stateMachine.currentState.AnimationFinishTrigger();
    public IEnumerator SetBusy(float duration)
    {
        isBusy = true;
        yield return new WaitForSeconds(duration);
        isBusy = false;
    }
    
    void OnMove (InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    
}
