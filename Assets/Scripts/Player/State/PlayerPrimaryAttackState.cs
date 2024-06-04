using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{   
    
    public int comboAttackCounter { get; private set; }
    
    private float lastAttackTime;
    private float comboWindow = 2;
    
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        if (comboAttackCounter > 2 || Time.time >= lastAttackTime + comboWindow)
            comboAttackCounter = 0;
        
        player.anim.SetInteger("ComboCounter", comboAttackCounter);
        
        float attackDirection = player.facingDirection;
        if (xInput != 0)
            attackDirection = xInput;
        
        player.SetVelocity(player.attackMovement[comboAttackCounter].x * attackDirection, 
            player.attackMovement[comboAttackCounter].y);
        stateTimer = .1f;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 0)
            player.SetZeroVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
        
    }
    
    
    public override void Exit()
    {
        base.Exit();
        
        player.StartCoroutine("BusyFor", .15f);
        
        comboAttackCounter++;
        lastAttackTime = Time.time;
    }
    
}
