using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonBoxDamageController : MonoBehaviour
{
    [SerializeField] private ChameleonController _chameleonController;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _chameleonController.IsAttack = true;
            _chameleonController.Target = collision.gameObject.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _chameleonController.IsAttack = true;
            _chameleonController.Target = collision.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _chameleonController.IsAttack = false;
            _chameleonController.Target = Vector2.zero;
        }
    }
}
