
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetect:MonoBehaviour,ILogicUpdate
{
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public LayerMask EnemyLayer;
    public LayerMask TrapLayer;


    public Transform CheckLeft;
    public Transform CheckRight;
    public Transform CheckTop;
    public Transform CheckBottom;


    public Vector2 CheckBottomSize;
    public Vector2 CheckLeftSize;
    public Vector2 CheckRightSize;
    public Vector2 CheckTopSize;

    private BoxCollider2D _boxCollider2D;

    public bool IsGround=false;
    public bool IsWallTop= false;
    public bool IsWallLeft = false;
    public bool IsWallRight = false;

    public bool IsEnemyLeft = false;
    public bool IsEnemyRight = false;
    public bool IsEnemyTop = false;
    public bool IsEnemyBottom = false;

    public bool IsTrapLeft =false;
    public bool IsTrapRight =false;
    public bool IsTrapTop =false;
    public bool IsTrapBottom =false;

    private void Start()
    {
        _boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
    }

    public void LogicUpdate()
    {
        IsGround = Ground;
        IsWallTop = WallTop;
        IsWallLeft = WallLeft;
        IsWallRight = WallRight;

        IsEnemyLeft = EnemyLeft;
        IsEnemyRight = EnemyRight;
        IsEnemyTop = EnemyTop;
        IsEnemyBottom = EnemyBottom;

        IsTrapLeft = TrapLeft;
        IsTrapRight = TrapRight;
        IsTrapTop = TrapTop;    
        IsTrapBottom = TrapBottom;
    }

    public bool Ground
    {
        get =>Physics2D.OverlapBox(CheckBottom.position, CheckBottomSize,0f, GroundLayer);
    }
    public bool WallTop
    {
        get => Physics2D.OverlapBox(CheckRight.position,CheckRightSize,0f,WallLayer);
    }
    public bool WallLeft
    {
        get => Physics2D.OverlapBox(CheckLeft.position, CheckLeftSize,0f, WallLayer);
    }

    public bool WallRight
    {
        get => Physics2D.OverlapBox(CheckRight.position, CheckRightSize,0f, WallLayer);
    }

    public bool EnemyLeft
    {
        get => Physics2D.OverlapBox(CheckLeft.position, CheckLeftSize,0f, EnemyLayer);
    }

    public bool EnemyRight
    {
        get => Physics2D.OverlapBox(CheckRight.position, CheckRightSize,0f, EnemyLayer);
    }

    public bool EnemyTop
    { 
        get => Physics2D.OverlapBox(CheckTop.position,CheckTopSize,0f, EnemyLayer);
    }
    public bool EnemyBottom
    {
        get => Physics2D.OverlapBox(CheckBottom.position, CheckBottomSize, 0f, EnemyLayer);
    }

    public bool TrapLeft
    {
        get => Physics2D.OverlapBox(CheckLeft.position, CheckLeftSize, 0f, TrapLayer);
    }

    public bool TrapRight
    {
        get => Physics2D.OverlapBox(CheckRight.position, CheckRightSize, 0f, TrapLayer);
    }

    public bool TrapTop
    {
        get => Physics2D.OverlapBox(CheckTop.position, CheckTopSize, 0f, TrapLayer);
    }
    public bool TrapBottom
    {
        get => Physics2D.OverlapBox(CheckBottom.position, CheckBottomSize, 0f, TrapLayer);
    }





    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(CheckBottom.position, CheckBottomSize);
        Gizmos.DrawWireCube(CheckLeft.position, CheckLeftSize);
        Gizmos.DrawWireCube(CheckRight.position, CheckRightSize);
        Gizmos.DrawWireCube(CheckTop.position, CheckTopSize);
    }
}