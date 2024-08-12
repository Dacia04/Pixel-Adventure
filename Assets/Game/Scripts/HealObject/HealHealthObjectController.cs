using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHealthObjectController : MonoBehaviour
{

    public AudioSource audioSource {get;private set;} 
    public AudioSystem audioSystem {get;private set;}
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player _player = collision.gameObject.GetComponent<Player>();

            if(_player.CurrentHealth < _player.playerData.Health)
            {
                _player.IncreaseHealth(1);
                gameObject.GetComponent<Animator>().SetBool("Collected",true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                audioSystem.PlayCollectHealObjectAudio(audioSource);
                Destroy(gameObject,2f);
            }
        }
    }
}
