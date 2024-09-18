using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed* 0.8f, rb.velocity.y);
        }
        if (rb.velocity.y < 0.01f )
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
}
