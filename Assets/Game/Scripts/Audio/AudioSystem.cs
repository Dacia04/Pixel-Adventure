using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem: MonoBehaviour
{
    [SerializeField] private AudioSourceData audioSourceData;
    
    public void PlayMoveAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.MoveAudio;
        audioSource.Play();
    }
    public void PlayJumpAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.JumpAudio;
        audioSource.Play();
    }
    public void PlayClimbAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.ClimbAudio;
        audioSource.Play();
    }
    public void PlayHitAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.HitAudio;
        audioSource.Play();
    }
    public void PlayDeathAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.DeathAudio;
        audioSource.Play();
    }
    public void PlayGameMenuBackGroundAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.GameMenuBackgroundAudio;
        audioSource.Play();
    }
    public void PlayInGameAudio1(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.InGameAudio1;
        audioSource.Play();
    }
    public void PlayInGameAudio2(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.InGameAudio2;
        audioSource.Play();
    }
    public void PlayInGameAudio3(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.InGameAudio3;
        audioSource.Play();
    }
    public void PlayClickButtonAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.ClickButtonAudio;
        audioSource.Play();
    }
    public void PlayCompleteLevelAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.CompleteLevelAudio;
        audioSource.Play();
    }
    public void PlayCollectHealObjectAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.CollectHealObjectAudio;
        audioSource.Play();
    }
    public void PlayCollectStarAudio(AudioSource audioSource)
    {
        audioSource.clip = audioSourceData.CollectStarAudio;
        audioSource.Play();
    }

    public void PlayNull(AudioSource audioSource)
    {
        audioSource.clip = null;
    }

    public void ChangeMasterVolumn(float value)
    {
        audioSourceData.MasterVolumn.audioMixer.SetFloat("master",value-80);
    }
    public void ChangeBGMusicVolumn(float value)
    {
        audioSourceData.BackGroundMusicVolumn.audioMixer.SetFloat("music",value-80);
    }
    public void ChangeVFXVolumn(float value)
    {
        audioSourceData.VFXVolumn.audioMixer.SetFloat("vfx",value-80);
    }

    public void LoadVolumn()
    {
        List<float> vol = SaveLoadSystem.LoadVolumn();
        ChangeMasterVolumn(vol[0]);
        ChangeBGMusicVolumn(vol[1]);
        ChangeVFXVolumn(vol[2]);
    }
}
