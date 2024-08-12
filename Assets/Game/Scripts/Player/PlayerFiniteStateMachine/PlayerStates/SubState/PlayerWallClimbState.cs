using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

    }
    public override void Enter()
    {
        base.Enter();
        player.audioSystem.PlayClimbAudio(player.audioSource);
        player.playerCollsion.CanWallClimb = false;

        player.movement.SetVelocityZero();
    }

    public override void Exit()
    {
        player.audioSystem.PlayNull(player.audioSource);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if(yInput !=0)
        {
            player.movement.SetVelocityY(playerData.ClimbVelocity * yInput);
        }
        else
        {
            player.movement.SetVelocityY(0.5f);
        }
        
        
        
        
        base.LogicUpdate();

        
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    
}