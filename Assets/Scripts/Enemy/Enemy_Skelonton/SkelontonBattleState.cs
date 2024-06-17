using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelontonBattleState : EnemyState
{
    private Transform target; // Player
    private Enemy_Skelonton skelonton;
    private int moveDirection;
    
    public SkelontonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton skelonton) : base(stateMachine, enemyBase, animBoolName)
    {
        this.skelonton = skelonton;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (skelonton.IsPlayerDetected())
        {
            stateTimer = skelonton.battleTime;
            
            if (skelonton.IsPlayerDetected().distance < skelonton.atkDistance)
                if (CanAttack())
                    stateMachine.ChangeState(skelonton.attackState);
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(target.transform.position, skelonton.transform.position) > 10)
                stateMachine.ChangeState(skelonton.idleState);
        }
        
        if (target.position.x > skelonton.transform.position.x)
            moveDirection = 1;
        else if (target.position.x < skelonton.transform.position.x)
            moveDirection = -1;
        
        skelonton.SetVelocity(skelonton.moveSpeed * moveDirection, rb.velocity.y);
    }
    
    public override void Enter()
    {
        base.Enter();
        
        target = PlayerManager.instance.player.transform;
    }
    
    public override void Exit()
    {
        base.Exit();
    }
    
    private bool CanAttack()
    {
        if (Time.time >= skelonton.lastTimeAttacked + skelonton.atkCooldown)
        {
            skelonton.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
