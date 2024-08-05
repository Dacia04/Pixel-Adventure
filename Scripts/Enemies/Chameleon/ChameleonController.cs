using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonController : PosMoveEnemy
{
    public float TimeReset;
    public float TimeResetAttack;
    public BoxCollider2D HitBox; 
    public float Distance;

    private bool _isRun;
    private bool _isTangHinh;
    private bool _isAttack;
    private bool _isAttacked= false;

    private float _currentSpeed;

    private Vector2 _target;


    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public Vector2 Target { get => _target; set => _target = value; }


    protected override void Start()
    {
        base.Start();
        SetTangHinhState();

        _currentSpeed = Speed;
        HitBox.enabled = false;
        StartCoroutine(ResetTangHinh());
        StartCoroutine(AttackCoroutine());
    }




    protected override void Update()
    {
        base.Update();
        base.AnimationUpdate();
        Attack();

        _animator.SetBool("Run", _isRun);
        _animator.SetBool("TangHinh", _isTangHinh);

    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player _player = collision.gameObject.GetComponent<Player>();
            if (_player.collisionDetect.IsEnemyBottom && !_requireNewAttack)
            {
                DecreaseHealth(1);
                ResetMove();
                _animator.SetTrigger("Hit");
                _requireNewAttack = true;
                SetRunState();
            }
        }


    }


    

    private void Attack()
    {
        if(_isAttack)
        {
            if (_isFacingRight && ( (Target.x - transform.position.x) <= Distance ) && (Target.x > transform.position.x) && !_isAttacked)
            {
                _animator.SetTrigger("Attack");
                _isAttacked = true;
            }
            else if (!_isFacingRight && ( (transform.position.x - Target.x) <= Distance ) && (Target.x < transform.position.x) && !_isAttacked)
            {
                _animator.SetTrigger("Attack");
                _isAttacked = true;
            }

        }       
    }

    #region Animation Event
    private void EnableHitBox()
    {
        HitBox.enabled = true;
    }
    private void DisableHitBox()
    {
        HitBox.enabled = false;
    }

    private void StopMove()
    {
        Speed = 0;
    }
    private void ResetMove()
    {
        Speed = _currentSpeed;
    }
    #endregion

    private void SetRunState()
    {
        _isRun = true;
        _isTangHinh = false;
    }
    private void SetTangHinhState()
    {
        _isRun = false;
        _isTangHinh = true;
    }

    private IEnumerator ResetTangHinh()
    {
        while(true)
        {
            if(!_isAttack && _isRun)
            {
                yield return new WaitForSeconds(TimeReset);
                SetTangHinhState();
            }
            yield return null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while(true)
        {
            if(_isAttacked)
            {
                yield return new WaitForSeconds(TimeResetAttack);
                _isAttacked = false;
            }
            yield return null;
        }
    }
}
