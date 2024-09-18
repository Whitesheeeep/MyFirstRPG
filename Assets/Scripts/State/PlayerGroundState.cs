using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
            
        }

        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.InAirState);
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) 
        { 
            stateMachine.ChangeState(player.JumpState);
        }
    }
}
