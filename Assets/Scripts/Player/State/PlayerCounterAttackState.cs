using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{   
    private bool canCreateClone;
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();
        
        player.SetZeroVelocity();
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (Collider2D hit in hitEnemies)
        {
            if(hit.GetComponent<Enemy>()!=null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10f; // to prevent the state from changing
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    player.skillManager.parrySkill.UseSkill(); // to prevent the player from using the skill again
                    
                    if (canCreateClone)
                    {
                        canCreateClone = false;
                        player.skillManager.parrySkill.MakeMirageOnParry(player.transform);
                    }
                    
                }
            }
        }
        
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }
}
