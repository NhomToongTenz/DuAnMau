using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }
    
    public override void Update()
    {
        base.Update();
    
        
        if(Input.GetKeyDown(KeyCode.R) && player.skillManager.blackholeSkill.blackholeUnlocked)
            stateMachine.ChangeState(player.blackholeState);
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skillManager.swordSkill.swordUnlocked)
            stateMachine.ChangeState(player.aimSwordState);
        
        if(Input.GetKeyDown(KeyCode.Q) && player.skillManager.parrySkill.parryUnlocked)
            stateMachine.ChangeState(player.counterAttackState);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.PrimaryAttackStateState);
        }
        
        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.inAirState);
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }

       
    }
    
    private bool HasNoSword()
    {
        if(!player.sword)
            return true;
        
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        
        return false;
    }
    
    
    public override void Exit()
    {
        base.Exit();
    }
}
