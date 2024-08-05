using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    public float Force;
    public LayerMask playerLayer;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(rb.velocity.x, Force);
            _animator.SetTrigger("ArrowActive");

        }


    }

}
