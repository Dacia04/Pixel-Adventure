using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
    public override void Enter()
    {
        base.Enter();
        player.audioSystem.PlayMoveAudio(player.audioSource);
        player.audioSource.loop = true;
    }

    public override void Exit()
    {
        player.audioSystem.PlayNull(player.audioSource);
        player.audioSource.loop = false;
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.movement.LogicUpdate();
        
        if (!isExitingState)
        {
            if(xInput == 0f) 
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.movement.SetVelocityX(playerData.MovementVeclocity, player.InputHandler.NormInputX);
    }
}