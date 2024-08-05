using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosRockHeadController : MonoBehaviour
{
    public bool isTouchingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer= false;
        }
    }
}
