using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    private SceneScore SceneScore;

    public AudioSource audioSource {get;private set;} 
    public AudioSystem audioSystem {get;private set;}

    private void Start() {
        SceneScore = GameObject.FindWithTag("UI").GetComponent<SceneScore>();
        audioSource = GetComponent<AudioSource>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();
    }


    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.gameObject.CompareTag("Player"))
        {
            audioSystem.PlayCollectStarAudio(audioSource);
            SceneScore.StarScore++;
            gameObject.SetActive(false);

        }
    }
}
