 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        //rb.velocity = new Vector2(player.dashSpeed * player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = new Vector2(player.dashSmooth*player.dashDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }
}
