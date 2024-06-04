using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;
    
    protected bool triggerCalled;
    private string animBoolName;
    protected float stateTimer;
    
    public virtual void Update()
    {
        //DoChecks();
        stateTimer -= Time.deltaTime;
    }
    
    public EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase = enemyBase;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
        //DoChecks();
    }
    
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimBoolName(animBoolName);
    }
    
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    
}
