using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        if (player.IsGroundDetected())
        {
            player.rb.velocity = new Vector2(0, rb.velocity.y);//修复从空中跳跃下来会有速度的bug
            stateMachine.ChangeState(player.IdleState);
        }
        
        if (xInput != 0 )
            player.SetVelocity(xInput* 0.8f* player.moveSpeed, rb.velocity.y);

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }
}
