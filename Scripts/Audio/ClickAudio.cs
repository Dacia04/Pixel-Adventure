using UnityEngine;

public class ClickAudio : MonoBehaviour
{
    public AudioSource audioSource {get;private set;} 
    public AudioSystem audioSystem {get;private set;}

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();
    }

    public void PlayClickSound()
    {
        audioSystem.PlayClickButtonAudio(audioSource);
    }
}