using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    public float Force;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().movement.SetVelocityX(Force,1);
            _animator.SetTrigger("Hit");
        }
    }
}
