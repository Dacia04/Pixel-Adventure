using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{ 
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int temp = Random.Range(1,3);
        if(temp==1)
            GameObject.FindWithTag("Audio").GetComponent<AudioSystem>().PlayInGameAudio1(audioSource);
        else if(temp==2)
            GameObject.FindWithTag("Audio").GetComponent<AudioSystem>().PlayInGameAudio2(audioSource);
        else if(temp==3)
            GameObject.FindWithTag("Audio").GetComponent<AudioSystem>().PlayInGameAudio3(audioSource);
    }
}
