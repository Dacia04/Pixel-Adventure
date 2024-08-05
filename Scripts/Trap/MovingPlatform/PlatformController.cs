using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Animator _animator;

    public float speed;
    public Transform A;
    public Transform B;
    private Vector2 target;

    void Start()
    {
        target = A.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, A.position) < 0.1f) target = B.position;
        if (Vector2.Distance(transform.position, B.position) < 0.1f) target = A.position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}
