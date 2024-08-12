using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoxDamage : MonoBehaviour
{
    public GhostManager Ghost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            Ghost.IsChase = true;
            Ghost.Target = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("stay");
            Ghost.IsChase = true;
            Ghost.Target = collision.transform.position;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            Ghost.IsChase = false;
        }
    }
}
