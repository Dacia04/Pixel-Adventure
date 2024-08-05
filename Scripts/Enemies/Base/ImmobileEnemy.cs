using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobileEnemy : EnemyBase
{ 
    public float TimeResetAttack;
    protected bool _isAttack;

    public bool IsAttack { get => _isAttack; set => _isAttack = value; }


    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackCoroutine());
    }

    protected IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (_isAttack)
            {
                _animator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(TimeResetAttack);
        }
    }
}
