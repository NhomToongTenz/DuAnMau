using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelontonAttackState : EnemyState
{
    private Enemy_Skelonton Skelonton;
    
    public SkelontonAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton skelonton) : base(stateMachine, enemyBase, animBoolName)
    {
        this.Skelonton = skelonton;
    }
    
    public override void Update()
    {
        base.Update();
        
        Skelonton.SetZeroVelocity();
        
        if(triggerCalled)
            stateMachine.ChangeState(Skelonton.battleState);
        
        
    }
    
    public override void Enter()
    {
        base.Enter();
    }
    
    public override void Exit()
    {
        base.Exit();
        Skelonton.lastTimeAttacked = Time.time;
    }
}
