using Unity.VisualScripting;
using UnityEngine;
public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    protected int xInput;
    protected int yInput;

    protected bool JumpInput;
    protected bool WallClimb;
    protected bool IsGround;
    protected bool IsWall;
    protected bool IsHit;

    protected float enernyLeft;


    public PlayerAbilityState(Player player, PlayerStateMachine playerStateMachine,PlayerData playerData,string animBoolName) : base (player,playerStateMachine,playerData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();

        IsGround = player.collisionDetect.IsGround;
        IsWall = (player.collisionDetect.IsWallRight || player.collisionDetect.IsWallLeft);
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.WallClimbStartTime = Time.time;

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
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


        if (isAbilityDone)
        {

            if (IsHit)
            {
                playerStateMachine.ChangeState(player.HitState);
            }
            else if (IsGround && player.Rb2D.velocity.y < 0.1f && !JumpInput)
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
            else if(!IsGround && player.Rb2D.velocity.y < -1f )
            {
                playerStateMachine.ChangeState(player.FallState);
            }
            else if(IsWall && WallClimb &&  ( (startTime-Time.time) > 0.1f ) && player.playerCollsion.CanWallClimb)
            {
                playerStateMachine.ChangeState(player.WallClimbState);
            }

            
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    
}