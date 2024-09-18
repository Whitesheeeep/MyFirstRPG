using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public bool isBusy { get; private set; } = false;
    [Header("Attack Info")]
    public Vector2[] attackMotion;

    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCoolDown;
    private float dashCoolTimer;
    public float dashSpeed;
    public float dashDuration;
    
    public float dashDir { get; private set; }
    public float dashSmooth;

    #region Collision Info
    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;//SerializeField 使私有变量在Inspector中显示
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    #endregion

    //面朝方向
    public int facingDir { get; private set; } = 1;
    private bool IsFaceRight = true;
    public float wallJumpDuration;


    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion Components

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    #endregion States

    private void Awake()
    {
        Application.targetFrameRate = 40;
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(StateMachine, this, "Idle");
        MoveState = new PlayerMoveState(StateMachine, this, "Move");
        JumpState = new PlayerJumpState(StateMachine, this, "Jump");
        InAirState = new PlayerInAirState(StateMachine, this, "Jump");
        DashState = new PlayerDashState(StateMachine, this, "Dash");
        WallSlideState = new PlayerWallSlideState(StateMachine, this, "WallSlide");
        WallJumpState = new PlayerWallJumpState(StateMachine, this, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(StateMachine, this, "Attack_1");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
        
    }
        
    private void Update()
    {
        StateMachine.currentState.Update();
        CheckForDashInput();
        //Debug.Log(StateMachine.currentState);
    }


    //一个协程，让isBusy在等待一段时间后变为false
    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public void CheckForDashInput()
    {
        dashCoolTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolTimer < 0)
        {
            dashCoolTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");  
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            StateMachine.ChangeState(DashState);
        }
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround); 
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        //旋转
        facingDir *= -1;
        IsFaceRight = !IsFaceRight;
        transform.Rotate(0, 180, 0 );
    }

    public void FlipController(float x)
    {
        if (x == 0) return;
        if (x < 0 && IsFaceRight)
        {
            Flip();
        }
        else if (x > 0 && !IsFaceRight)
        {
            Flip();
        }
    }

    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();
}
