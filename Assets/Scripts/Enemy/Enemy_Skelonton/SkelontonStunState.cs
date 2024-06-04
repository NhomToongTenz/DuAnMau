using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkelontonStunState : EnemyState
{
   private Enemy_Skelonton enemySkelonton;
   
   public SkelontonStunState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton enemySkelonton) : base(stateMachine, enemyBase, animBoolName)
   {
       this.enemySkelonton = enemySkelonton;
   }

   public override void Update()
   {
       base.Update();
        
       enemySkelonton.fx.InvokeRepeating("RedColorBlink", 0, .1f);
       if(stateTimer < 0)
           stateMachine.ChangeState(enemySkelonton.idleState);
       

   }

   public override void Enter()
   {
       base.Enter();
       
         stateTimer = enemySkelonton.stunDuration;
         
         rb.velocity = new Vector2(enemySkelonton.stunDirection.x * -enemySkelonton.facingDirection, enemySkelonton.stunDirection.y);
        
   }

   public override void Exit()
   {
       base.Exit();
         enemySkelonton.fx.Invoke("CancleColorChange", 0);
   }
}
