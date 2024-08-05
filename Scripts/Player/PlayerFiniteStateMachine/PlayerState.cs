using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerState : ILogicUpdate
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;
    protected PlayerData playerData;
    protected CollisionDetect collisionDetect;

    protected bool isAnimationFinishes;
    protected bool isExitingState;

    protected float startTime;

    public string _animBoolName;

    public PlayerState(Player player,PlayerStateMachine playerStateMachine,PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this._animBoolName = animBoolName;

        collisionDetect = player.collisionDetect;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(_animBoolName, true);
        startTime = Time.time;
        isAnimationFinishes = false;
        isExitingState = false;

    }

    public virtual void Exit()
    {
        player.Anim.SetBool(_animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck() { }

}