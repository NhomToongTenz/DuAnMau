using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
        
    
        public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
                base.Enter();
                
                player.skillManager.swordSkill.DotsActive(true);
                player.StartCoroutine("BusyFor", 0.2f);
        }

        public override void Update()
        {
                base.Update();
                        
                player.SetZeroVelocity();
                
                if (Input.GetKeyUp(KeyCode.Mouse1))
                        stateMachine.ChangeState(player.idleState);
                
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (player.transform.position.x > mousePos.x && player.facingDirection == 1) 
                        player.Flip();
                else if (player.transform.position.x < mousePos.x && player.facingDirection == -1)
                        player.Flip();
                        
                
                       
                
        }
}
