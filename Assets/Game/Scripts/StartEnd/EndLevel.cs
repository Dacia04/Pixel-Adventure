using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private SaveLoadManager saveLoad;
    public float TimeChangeScene;

    public AudioSource audioSource {get;private set;}
    public AudioSystem audioSystem {get;private set;}

    private void Start() {
        saveLoad = GameObject.FindWithTag("saveload").GetComponent<SaveLoadManager>();
        audioSource = GetComponent<AudioSource>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            audioSystem.PlayCompleteLevelAudio(audioSource);
            saveLoad.SaveGameCompleted();
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeChangeScene);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            yield return null;
        }
    }
}
