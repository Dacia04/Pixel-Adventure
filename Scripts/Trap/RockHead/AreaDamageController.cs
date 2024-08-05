using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageController : MonoBehaviour
{
    public bool dam;
    public Vector2 target;


    private void Start()
    {
        dam = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dam = true;
            target = collision.transform.position; 
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dam = false;
        }
    }
}
