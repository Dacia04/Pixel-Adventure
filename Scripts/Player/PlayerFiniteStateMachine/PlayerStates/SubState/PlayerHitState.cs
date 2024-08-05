using UnityEngine;

public class PlayerHitState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool JumpInput;
    protected bool WallClimb;
    protected bool IsGround;
    protected bool IsWall;

    protected float enernyLeft;

    public bool AnimationFinish;


    public PlayerHitState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        player.audioSystem.PlayHitAudio(player.audioSource);
    }

    public override void Exit()
    {
        player.audioSystem.PlayNull(player.audioSource);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        KnockBack();

        enernyLeft = player.EnergyLeft;

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        WallClimb = player.InputHandler.WallClimb;

        player.movement.SetVelocityX(playerData.MovementVeclocity, xInput);

            if (!IsGround && player.Rb2D.velocity.y < -1f)
            {
                playerStateMachine.ChangeState(player.FallState);
            }
            else if (IsGround && player.Rb2D.velocity.y < 1f)
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
            else if (IsWall && WallClimb && player.playerCollsion.CanWallClimb)
            {
                playerStateMachine.ChangeState(player.WallClimbState);
            }
            else if (JumpInput && enernyLeft > 1)
            {
                playerStateMachine.ChangeState(player.JumpState);
            }
            else if (JumpInput && enernyLeft == 1)
            {
                playerStateMachine.ChangeState(player.DoubleJumpState);
            }


        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }


    private void KnockBack()
    {
        
        if(player.Knockback.IsKnockBackActive == false && player.playerCollsion.IsHit)
        {
            player.playerCollsion.IsHit = false;
            player.Knockback.KnockBackTop();
        }
        
    }
}