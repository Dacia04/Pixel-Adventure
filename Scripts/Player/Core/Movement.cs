using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement: MonoBehaviour,ILogicUpdate
{
    private Rigidbody2D Rb2D;
    public Transform PlayerTransform;
    public bool IsFacingRight;

    public Vector2 Velocity { get; private set; }

    private void Awake()
    {
        Rb2D = GetComponentInParent<Rigidbody2D>();
        IsFacingRight = true;
    }

    public void LogicUpdate()
    {
        DirectionUpdate();
    }

    private void DirectionUpdate()
    {
        if (!IsFacingRight && Rb2D.velocity.x > 0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            PlayerTransform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (IsFacingRight && Rb2D.velocity.x < -0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            PlayerTransform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }



    //lay vector phai lam goc roi xoay goc
    public void SetVelocity(float speed,float angle)
    {
        Vector3 forceDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        Rb2D.velocity = forceDirection * speed;
    }
    public void SetVelovity(float speedX,float direction,float speedY)
    {
        Rb2D.velocity = new Vector2(speedX * direction, speedY);
    }
    
    public void SetVelocityX(float speed,int direction)
    {
        Rb2D.velocity = new Vector2(speed * direction, Rb2D.velocity.y);
    }

    public void SetVelocityY(float speed)
    {
        Rb2D.velocity = new Vector2(Rb2D.velocity.x, speed);
    }

    public void SetVelocityZero()
    {
        Rb2D.velocity = Vector2.zero;
    }
    public void SetVelocityXByZero()
    {
        Rb2D.velocity = new Vector2(0,Rb2D.velocity.y);
    }
}