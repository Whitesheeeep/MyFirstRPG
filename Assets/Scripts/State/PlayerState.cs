using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected Rigidbody2D rb;
    protected bool triggerCalled;

    #region Move
    protected float xInput;
    protected float yInput;
    protected float stateTimer;

    #endregion

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName )
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }

    //½øÈë×´Ì¬º¯Êý
    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", rb.velocity.y);
        stateTimer -= Time.deltaTime;
        if(stateTimer < -10000 )
        {
            stateTimer = -100;
        }
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
