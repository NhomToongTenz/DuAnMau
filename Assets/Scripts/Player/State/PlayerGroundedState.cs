

public class PlayerGroundedState : PlayerState
{
    
    protected bool jumpInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        input = player.inputHandler.movementInput;
        jumpInput = player.inputHandler.jumpInput;

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if (jumpInput && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        
        
        
        
    }
    
    
    
    
}

