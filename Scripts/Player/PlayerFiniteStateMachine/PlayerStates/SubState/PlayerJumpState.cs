using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    
    
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        DecreaseEnergy();

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

        if (enernyLeft==1 && JumpInput)
        {
            playerStateMachine.ChangeState(player.DoubleJumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public void ResetEnergy() => player.EnergyLeft = playerData.Energy;
    public void DecreaseEnergy() => player.EnergyLeft--;
    public void IncreaseEnergy() => player.EnergyLeft++;



}