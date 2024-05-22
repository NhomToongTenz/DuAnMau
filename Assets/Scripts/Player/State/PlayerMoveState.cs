using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        
        _player.SetVelocity(_movementInput.x * _player.moveSpeed, rb.velocity.y);
        if(_movementInput.x == 0 || _player.IsTouchingWall)
            _stateMachine.ChangeState(_player.idleState);
        
    }
    
}
