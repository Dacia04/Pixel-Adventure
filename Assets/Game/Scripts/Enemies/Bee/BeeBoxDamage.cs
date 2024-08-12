

using Unity.VisualScripting;
using UnityEngine;

public class BeeBoxDamage: MonoBehaviour
{
    public BeeController Bee;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Bee.IsAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bee.IsAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bee.IsAttack = false;
        }
    }
}