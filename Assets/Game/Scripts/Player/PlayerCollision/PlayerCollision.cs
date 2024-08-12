using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision: MonoBehaviour
{
    public bool CanWallClimb;
    public bool IsHit;

    private Player _player;
    


    private void Start()
    {
        CanWallClimb = true;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if( !_player.collisionDetect.IsWallLeft && !_player.collisionDetect.IsWallRight)
        {
            CanWallClimb = true;
        }
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            IsHit = true;
            if(collision.gameObject.GetComponent<BoxDamageController>() != null)
            {
                _player.DecreaseHealth(collision.gameObject.GetComponent<BoxDamageController>().Damage);
            }
            else
            {
                _player.DecreaseHealth(1);
            }
            
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(_player.collisionDetect.IsEnemyBottom)
            {
                _player.Knockback.Attack();
                //Debug.Log("attack");
            }
            else
            {
                IsHit = true;
                _player.DecreaseHealth(collision.gameObject.GetComponent<BoxDamageController>().Damage);
                //Debug.Log("hurt");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            IsHit = false;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            IsHit = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            IsHit = true;
            _player.DecreaseHealth(collision.GetComponent<BoxDamageController>().Damage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_player.collisionDetect.IsEnemyBottom)
            {
                _player.Knockback.Attack();
            }
            else
            {
                IsHit = true;
                _player.DecreaseHealth(collision.gameObject.GetComponent<BoxDamageController>().Damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            IsHit = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IsHit = false;
        }
    }


}