using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosMoveEnemy : EnemyBase
{
    public float Speed;
    public GameObject PosLeft;
    public GameObject PosRight; 

    protected int _moveDir;
    protected bool _isFacingRight = false;

    protected override void Start()
    {
        base.Start();
        _moveDir = -1;
    }

    protected override void Update()
    {
        base.Update();
        AnimationUpdate();
    }

    protected virtual void AnimationUpdate()
    {
        if (!_isFacingRight && _rigidbody2D.velocity.x > 0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
        else if (_isFacingRight && _rigidbody2D.velocity.x < -0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
    }

    protected void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(Speed * _moveDir, _rigidbody2D.velocity.y);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject == PosLeft)
        {
            _moveDir = 1;
        }
        if (collision.gameObject == PosRight)
        {
            _moveDir = -1;
        }

    }
}
