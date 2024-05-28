using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        //player.inputHandler.OnDashRelease();
        stateTimer = player.dashTimeDuration;
        
    }

    public override void Exit()
    {
        base.Exit();
        
        player.SetVelocity(0f, rb.velocity.y);
        
       
        
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);
        
        if(stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
        
    }
}
