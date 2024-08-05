

using UnityEngine;
public class EnemyBase : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public int MaxHealth; 


    protected Animator _animator;
    protected BoxCollider2D _boxCollider2D;
    protected Rigidbody2D _rigidbody2D;

    protected int _currentHealth;
    protected bool _requireNewAttack;

    protected virtual void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _currentHealth = MaxHealth;
    }

    protected virtual void Update()
    {
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player _player = collision.gameObject.GetComponent<Player>();
            if(_player.collisionDetect.IsEnemyBottom && !_requireNewAttack)
            {
                Debug.Log("player");
                DecreaseHealth(1);
                _animator.SetTrigger("Hit");
                _requireNewAttack = true;
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _requireNewAttack = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
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
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _requireNewAttack = false;
        }
    }

    protected void Death()
    {
        Destroy(gameObject);
    }

    public void DecreaseHealth(int damage)
    {
        _currentHealth -= damage;
    }
    
}