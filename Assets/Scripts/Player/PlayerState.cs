using UnityEngine.InputSystem   ;
using UnityEngine;

public class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected string _animBoolName;

    //protected Animator anim;
    protected Vector2 _movementInput;
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        _player.anim.SetBool(_animBoolName, true);
        rb = _player.rb;
        triggerCalled = false;
    }

    public virtual void Exit()
    {
        _player.anim.SetBool(_animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        _movementInput = _player.movementInput;
        _player.anim.SetFloat("yVelocity", rb.velocity.y);
    }
    
    
   
    

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
    
    
