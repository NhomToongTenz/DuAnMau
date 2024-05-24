using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    { 
        base.Update();
        
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
        if(input.x != 0)
           player.SetVelocity(player.moveSpeed *.8f * input.x, rb.velocity.y);
        
        
    }
}