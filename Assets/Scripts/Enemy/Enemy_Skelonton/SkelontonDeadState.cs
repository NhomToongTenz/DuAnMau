using UnityEngine;

public class SkelontonDeadState : EnemyState
{
    private Enemy_Skelonton enemySkelonton;

    public SkelontonDeadState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skelonton enemySkelonton) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemySkelonton = enemySkelonton;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
    }

    public override void Enter()
    {
        base.Enter();
        enemySkelonton.anim.SetBool(enemySkelonton.lastAnimBoolName, true);
        enemySkelonton.anim.speed = 0;
        enemySkelonton.csCol.enabled = false;

        stateTimer = .15f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
