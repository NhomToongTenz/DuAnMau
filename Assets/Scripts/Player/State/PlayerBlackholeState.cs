using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    
    private float flyTime = 0.5f;  
    private bool isFlying;
    private float defaultGravityScale;
    
    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        defaultGravityScale = player.rb.gravityScale;
        
        isFlying = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravityScale;
        player.fx.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        if (stateTimer < 0 && !isFlying)
        {
            rb.velocity = new Vector2(0, 0);
            if(player.skillManager.blackholeSkill.CanUseSkill())
                isFlying = true;
        }
        
        if(player.skillManager.blackholeSkill.SkillCompleted())
        {
            stateMachine.ChangeState(player.inAirState);
        }
        
    }
    
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
    }
}
