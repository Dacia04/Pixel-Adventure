using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    public float Speed;

    protected Vector2 _originalPosition;
    protected Vector2 _target;

    protected bool _isFacingRight = false;

    public Vector2 OriginalPosition { get => _originalPosition; }
    public Vector2 Target { get => _target; set => _target = value; }

    protected override void Start()
    {
        base.Start();
        _originalPosition = transform.position;
        _target = _originalPosition;
    }

    protected override void Update()
    {
        base.Update();
        AnimationUpdate();
        transform.position = Vector2.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
    }


    protected virtual void AnimationUpdate()
    {
        if (!_isFacingRight && (transform.position.x - _target.x) > 0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
        else if (_isFacingRight && (transform.position.x - _target.x) < -0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
    }


}
