using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCantDestroyController : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    public LayerMask playerLayer;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(IsTop())
            {
                _animator.SetTrigger("HitTop");
            }
            else if(IsSide())
            {
                _animator.SetTrigger("HitSide");
            }
        }
    }

    private bool IsTop()
    {
        return (Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x - 0.1f, _boxCollider2D.bounds.size.y), 0f, Vector2.up, .1f, playerLayer)
                            || Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x - 0.1f, _boxCollider2D.bounds.size.y), 0f, Vector2.down, .1f, playerLayer) );

    }

    private bool IsSide()
    {
        return (Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x, _boxCollider2D.bounds.size.y - 0.1f), 0f, Vector2.up, .1f, playerLayer)
                        || Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x, _boxCollider2D.bounds.size.y - 0.1f), 0f, Vector2.up, .1f, playerLayer));

    }
}
