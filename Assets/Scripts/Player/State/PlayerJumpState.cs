using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
            
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce); //jumpForce is a float variable that is set to 10 in the Player script
    }
    
    public override void Update()
    {
        base.Update();
        //if the player is falling, change to the inAirState
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.inAirState);
        
    
    }

    public override void Exit()
    {
        base.Exit();
    }
}
