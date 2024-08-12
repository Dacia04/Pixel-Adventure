using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> Pos;


    private Vector2 target;
    private int length;
    private int i;

    void Start()
    {
        i = 0;
        length = Pos.Count;
        target = Pos[0].position;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position,target) <0.1f)
        {
            if(i< (length-1))
            {
                target = Pos[++i].position;
            }
            else
            {
                i = 0;
                target = Pos[i].position;
            }
        }
               
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }



}
