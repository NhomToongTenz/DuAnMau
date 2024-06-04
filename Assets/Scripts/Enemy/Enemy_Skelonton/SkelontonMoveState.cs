using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelontonMoveState : SkelontonGroundedState
{
    public SkelontonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton enemySkelonton) : base(stateMachine, enemyBase, animBoolName, enemySkelonton)
    {
    }
    
    public override void Update()
    {
        base.Update();
        Skelonton.SetVelocity(Skelonton.moveSpeed * Skelonton.facingDirection, rb.velocity.y);
        
        if (Skelonton.IsWallDetected() || !Skelonton.IsGroundDetected())
        {   
            Skelonton.SetZeroVelocity();
            Skelonton.Flip();
            stateMachine.ChangeState(Skelonton.idleState);
        }
         
    }
    
    public override void Enter()
    {
        base.Enter();
        
        
        
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
