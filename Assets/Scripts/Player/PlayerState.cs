
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected string _animBoolName;
    
    protected Vector2 input;
    //protected Animator anim;
    
    
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(_animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(_animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }
    
    
   
    

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
    
    
