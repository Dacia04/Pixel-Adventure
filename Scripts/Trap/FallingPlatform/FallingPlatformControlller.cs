using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformControlller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private GameObject playerGamePbject;
    public float speed;
    public float timeLife;
    public Transform A;
    public Transform B;
    private Vector2 target;
    private float timeRemain;
    public float rotationSpeed;
    private bool coolDown = false;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        timeRemain = timeLife;
        target = A.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, A.position) < 0.1f) target = B.position;
        if (Vector2.Distance(transform.position, B.position) < 0.1f) target = A.position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);


        if(coolDown)
        {
            timeRemain -= Time.deltaTime;

            if(timeRemain <0)
            {
                DisableBoxcollider();
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _animator.SetTrigger("Falling");
                transform.Rotate(new Vector3(0, 0, 3), rotationSpeed * Time.deltaTime);
                Destroy(transform.parent.gameObject, 5f);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            coolDown = true;
            playerGamePbject = collision.gameObject;
        }
    }

    private void DisableBoxcollider()
    {
        playerGamePbject.transform.parent = null;
        _boxCollider2D.enabled = false;
    }






}
