using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : ChaseEnemy
{

    private bool _isAttack=false;
    private bool _isFly=false;
    private bool _isIdle=true;
    public bool IsAttack { get => IsAttack; set => _isAttack = value; }

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
    }

    protected override void AnimationUpdate()
    {
        base.AnimationUpdate();
        if (_isAttack)
        {
            _isIdle = false;
            _isFly = true;
        }
        if(!_isAttack)
        {
            _target = _originalPosition;
            if((Vector2)transform.position == _originalPosition)
            {
                _isIdle = true;
                _isFly = false;
                _currentHealth = MaxHealth;
            }
        }
        _animator.SetBool("Idle", _isIdle);
        _animator.SetBool("Fly", _isFly);

    }
}
