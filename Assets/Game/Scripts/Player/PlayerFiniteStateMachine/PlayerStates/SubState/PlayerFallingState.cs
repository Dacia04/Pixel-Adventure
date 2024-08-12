using UnityEngine;

public class PlayerFallingState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool JumpInput;
    protected bool WallClimb;
    protected bool IsGround;
    protected bool IsWall;

    protected float enernyLeft;
    protected bool IsHit;

    public bool IsFalling;


    public PlayerFallingState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        IsGround = player.collisionDetect.IsGround;
        IsWall = (player.collisionDetect.IsWallRight || player.collisionDetect.IsWallLeft);
    }
    public override void Enter()
    {
        base.Enter();
        IsFalling = true;
    }

    public override void Exit()
    {
        base.Exit();
        IsFalling = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enernyLeft = player.EnergyLeft;

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        WallClimb = player.InputHandler.WallClimb;

        IsHit = player.IsHit;

        player.movement.SetVelocityX(playerData.MovementVeclocity, xInput);
        
        if(IsHit)
        {
            playerStateMachine.ChangeState(player.HitState);
        }
        if (IsGround && player.Rb2D.velocity.y < 0.1f)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
        else if (IsWall && WallClimb && player.playerCollsion.CanWallClimb)
        {
            playerStateMachine.ChangeState(player.WallClimbState);
        }
        else if(JumpInput && enernyLeft>1)
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if(JumpInput && enernyLeft==1)
        {
            playerStateMachine.ChangeState(player.DoubleJumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}