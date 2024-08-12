using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool JumpInput;
    protected bool WallClimb;
    protected bool IsGround;
    protected bool IsWall;
    protected int energyLeft;
    protected bool IsHit;

    public PlayerTouchingWallState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        player.JumpState.IncreaseEnergy();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckWallClimbHoldTime();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        WallClimb = player.InputHandler.WallClimb;

        IsHit = player.IsHit;

        energyLeft = player.EnergyLeft;

        if(IsHit)
        {
            playerStateMachine.ChangeState(player.HitState);
        }
        else if (!IsWall)
        {
            playerStateMachine.ChangeState(player.FallState);
        }
        else if (JumpInput && energyLeft > 1)
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if (JumpInput && energyLeft == 1)
        {
            playerStateMachine.ChangeState(player.DoubleJumpState);
        }
        else if (IsGround && player.Rb2D.velocity.y < 0.1f)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
        else if(!WallClimb)
        {
            playerStateMachine.ChangeState(player.FallState);
        }      
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private void CheckWallClimbHoldTime()
    {
        if (Time.time > (player.InputHandler.WallClimbStartTime + playerData.TimeClimb))
        {
            playerStateMachine.ChangeState(player.FallState);
        }
    }


}