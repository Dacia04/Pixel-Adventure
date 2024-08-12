using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RockHeadSideController : MonoBehaviour
{
    public float force;
    public float timeResetAttack;
    public GameObject A;
    public GameObject B;
    public AreaDamageController areaDamage;

    private Rigidbody2D _rigidbody2D;
    private int attackDir;
    private bool isTouchingPlayer;
    private Animator _animator;

    private PosRockHeadController _posRockHeadControllerA;
    private PosRockHeadController _posRockHeadControllerB;

    private Player _player;
    private bool change= false;



    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _posRockHeadControllerA = A.GetComponent<PosRockHeadController>();
        _posRockHeadControllerB = B.GetComponent<PosRockHeadController>();
        _animator = GetComponent<Animator>();
        attackDir=-1;
        StartCoroutine(UpdateAttackDirection());
    }



    private void Update()
    {
        if(areaDamage.dam)
        {
            _rigidbody2D.velocity = new Vector2(force * attackDir,0);
        }
        CheckDeath();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            _rigidbody2D.velocity = Vector2.zero;
            _animator.SetTrigger("Hit");
        }      
        if(collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
            isTouchingPlayer = true;

            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == A || collision.gameObject == B)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _animator.SetTrigger("Hit");
            attackDir = -attackDir;
            change = true;
        }
    }


    private IEnumerator UpdateAttackDirection()
    {
        while (true)
        {
            if(change)
            {
                int temp = attackDir;
                attackDir =0;
                yield return new WaitForSeconds(timeResetAttack);
                change = false;
                attackDir = temp;
            }
            yield return null;
        }
        
    }

    private void CheckDeath()
    {
        if( (_posRockHeadControllerA.isTouchingPlayer && isTouchingPlayer)   || (_posRockHeadControllerB.isTouchingPlayer && isTouchingPlayer)  )
        {
            _player.DecreaseAllHealth();
        }
    }


}
