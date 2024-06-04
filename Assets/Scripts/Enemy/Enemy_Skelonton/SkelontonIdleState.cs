using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelontonIdleState : SkelontonGroundedState
{
    public SkelontonIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton enemySkelonton) : base(stateMachine, enemyBase, animBoolName, enemySkelonton)
    {
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(Skelonton.moveState);
        }

        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Skelonton.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
