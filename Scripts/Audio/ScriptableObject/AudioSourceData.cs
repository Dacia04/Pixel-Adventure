using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "newAudioSourceData", menuName = "Data/AudioSourceData")]
public class AudioSourceData : ScriptableObject
{
    [Header("Audio Player")]
    public AudioClip MoveAudio;
    public AudioClip JumpAudio;
    public AudioClip ClimbAudio;
    public AudioClip HitAudio;
    public AudioClip DeathAudio;

    [Header("Background Music")]
    public AudioClip GameMenuBackgroundAudio;
    public AudioClip InGameAudio1;
    public AudioClip InGameAudio2;
    public AudioClip InGameAudio3;

    [Header("Other")]
    public AudioClip ClickButtonAudio;
    public AudioClip CompleteLevelAudio;
    public AudioClip CollectHealObjectAudio;
    public AudioClip CollectStarAudio;

    [Header("Audio Mixer")]
    public AudioMixerGroup MasterVolumn;
    public AudioMixerGroup BackGroundMusicVolumn;
    public AudioMixerGroup VFXVolumn;

}
