using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJumpState);
            //stateMachine.ChangeState(player.JumpState);
            return;
        }


        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if(!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.InAirState);
        }

        if(yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }
    }
}
