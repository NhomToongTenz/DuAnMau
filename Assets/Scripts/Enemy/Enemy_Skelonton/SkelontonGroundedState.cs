using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelontonGroundedState : EnemyState
{
    protected Enemy_Skelonton Skelonton;
    protected Transform player;
    public SkelontonGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton enemySkelonton) : base(stateMachine, enemyBase, animBoolName)
    {
        this.Skelonton = enemySkelonton;
    }
    
    public override void Update()
    {
        base.Update();
        
        if(Skelonton.IsPlayerDetected() || Vector2.Distance(Skelonton.transform.position, player.position) < 2)
            stateMachine.ChangeState(Skelonton.battleState);
    }
    
    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;
       // rb.velocity = Vector2.zero;
        
        player = PlayerManager.instance.player.transform;

    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
