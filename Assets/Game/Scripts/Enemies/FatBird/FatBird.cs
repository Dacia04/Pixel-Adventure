using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : EnemyBase
{
    private FatBirdBoxDamage fatBirdBoxDamage;
    public float speed;


    protected override void Start()
    {
        base.Start();
        fatBirdBoxDamage = GetComponentInChildren<FatBirdBoxDamage>();
    }

    protected override void Update()
    {
        base.Update();
        if(fatBirdBoxDamage.attack)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = speed;
            _animator.SetTrigger("Attack");
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            if(_player.collisionDetect.IsEnemyBottom && !_requireNewAttack)
            {
                DecreaseHealth(1);
                _animator.SetTrigger("Hit");
                _requireNewAttack = true;
            }
            Death();
        }
        if(other.gameObject.CompareTag("Map"))
        {
            _animator.SetTrigger("Ground");
        }
    }

}
