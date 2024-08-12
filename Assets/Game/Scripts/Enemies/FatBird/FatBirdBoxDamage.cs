using UnityEngine;
using UnityEngine.Timeline;

public class FatBirdBoxDamage : MonoBehaviour
{
    public bool attack {get; private set;}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            attack = true;
        }
    }
}