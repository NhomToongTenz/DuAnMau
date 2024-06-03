
public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(player.moveSpeed * input.x, rb.velocity.y);
        
       if(input.x == 0f || player.IsTouchingWall())
           stateMachine.ChangeState(player.idleState);
        
    }
    
}
