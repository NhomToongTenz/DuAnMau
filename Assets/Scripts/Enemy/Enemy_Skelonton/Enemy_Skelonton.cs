using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skelonton : Enemy
{
    #region States
    public SkelontonIdleState idleState { get; private set; }
    public SkelontonMoveState moveState { get; private set; }
    public SkelontonBattleState battleState { get; private set; }
    public SkelontonAttackState attackState { get; private set; }
    public SkelontonStunState stunState { get; private set; }
    public SkelontonDeadState deadState { get; private set; }
    #endregion
    
    
    protected override void Awake()
    {
        base.Awake();
        
        idleState = new SkelontonIdleState(stateMachine,this, "Idle", this);
        moveState = new SkelontonMoveState( stateMachine,this, "Move", this);
        battleState = new SkelontonBattleState(stateMachine, this, "Move", this);
        attackState = new SkelontonAttackState(stateMachine, this, "Attack", this);
        stunState = new SkelontonStunState(stateMachine, this, "Stunned", this);
        deadState = new SkelontonDeadState(stateMachine, this, "Idle", this);
    }
    
    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(stunState);
    }
    
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    
    public override bool CanBeStunned()
    {
        
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
