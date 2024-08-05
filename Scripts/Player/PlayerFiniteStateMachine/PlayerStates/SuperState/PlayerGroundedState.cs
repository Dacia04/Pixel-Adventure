using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool JumpInput;
    protected bool WallClimb;
    protected bool IsGround;
    protected bool IsWall;
    protected bool IsHit;



    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        player.JumpState.ResetEnergy();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        WallClimb = player.InputHandler.WallClimb;
        IsHit = player.IsHit;

        if(IsHit)
        {
            playerStateMachine.ChangeState(player.HitState);
        }
        else if (JumpInput)
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if(!IsGround && player.Rb2D.velocity.y <-1f)
        {
            playerStateMachine.ChangeState(player.FallState);
        }
        else if(IsWall && WallClimb && player.playerCollsion.CanWallClimb)
        {
            playerStateMachine.ChangeState(player.WallClimbState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}