
using UnityEngine;
public class PlayerDoubleJumpState : PlayerAbilityState
{
    public PlayerDoubleJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        
    }
    public override void Enter()
    {
        base.Enter();
        player.audioSystem.PlayJumpAudio(player.audioSource);
        player.InputHandler.UseJumpInput();
        player.movement.SetVelocityY(playerData.JumpVelocity);
        isAbilityDone = true;
        player.JumpState.DecreaseEnergy();
    }

    public override void Exit()
    {
        player.audioSystem.PlayNull(player.audioSource);
        base.Exit();    
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.movement.SetVelocityX(playerData.MovementVeclocity, xInput);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}