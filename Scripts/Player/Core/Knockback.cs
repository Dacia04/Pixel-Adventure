
using System.Collections;
using UnityEngine;

public class Knockback: MonoBehaviour,ILogicUpdate
{
    public float KnockBackForce;
    public float KnockBackTime;

    public bool IsKnockBackActive;

     
    private float StartTimeKnockBack;

    private Player player;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        player = GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Attack()
    {
        IsKnockBackActive = true;
        StartTimeKnockBack = Time.time;
    }



    public void KnockBackTop()
    {
        
        IsKnockBackActive = true;
        boxCollider.enabled = false;
        StartTimeKnockBack = Time.time;
    }

    private void CheckTimeKnockBack()
    {
        if(IsKnockBackActive)
        {
            if(Time.time >= (StartTimeKnockBack + KnockBackTime))
            {
                IsKnockBackActive = false;
                boxCollider.enabled = true;
            }
           
        }
    }



    public void LogicUpdate()
    {
        if(IsKnockBackActive)
        {
            if (player.collisionDetect.TrapTop || player.collisionDetect.EnemyTop)
            {
                player.movement.SetVelocityY(KnockBackForce * -1/4);
            }
            else
            {
                player.movement.SetVelocityY(KnockBackForce);
            }
        }      
        CheckTimeKnockBack();
    }


}