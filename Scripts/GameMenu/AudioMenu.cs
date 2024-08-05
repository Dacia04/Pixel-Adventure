using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenu;

    private ClickAudio ClickSound;

    private Slider MasterVolumnSlider;
    private Slider BGMusicVolumnSlider;
    private Slider VFXVolumnSlider;

    public AudioSystem audioSystem {get;private set;}

    
    private void OnEnable() {
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();


        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MasterVolumnSlider = root.Q<VisualElement>("master-slider").Q<Slider>("music");
        root.Q<VisualElement>("master-slider").Q<Label>("Text").text ="MAS";
        BGMusicVolumnSlider = root.Q<VisualElement>("music-slider").Q<Slider>("music");
        root.Q<VisualElement>("music-slider").Q<Label>("Text").text ="BGM";
        VFXVolumnSlider = root.Q<VisualElement>("VFX-slider").Q<Slider>("music");
        root.Q<VisualElement>("VFX-slider").Q<Label>("Text").text ="VFX ";

        root.Q<Button>("back-button").clicked += () => BackAction();

        MasterVolumnSlider.RegisterCallback<ChangeEvent<float>>(OnMasterVolumnSliderValueChange);
        BGMusicVolumnSlider.RegisterCallback<ChangeEvent<float>>(OnBGMuicVolumnSliderValueChange);
        VFXVolumnSlider.RegisterCallback<ChangeEvent<float>>(OnVFXVolumnChangeValueSlider);

        List<float> vol = SaveLoadSystem.LoadVolumn();
        MasterVolumnSlider.value = vol[0];
        BGMusicVolumnSlider.value = vol[1];
        VFXVolumnSlider.value = vol[2];
        audioSystem.LoadVolumn();
    }

    private void BackAction()
    {
        ClickSound.PlayClickSound();
        gameObject.SetActive(false);
        SettingMenu.SetActive(true);
    }


    private void OnMasterVolumnSliderValueChange(ChangeEvent<float> value)
    {
        audioSystem.ChangeMasterVolumn(value.newValue);
        SaveLoadSystem.SaveMasVolumn(value.newValue);
    }

    private void OnBGMuicVolumnSliderValueChange(ChangeEvent<float> value)
    {
        audioSystem.ChangeBGMusicVolumn(value.newValue);
        SaveLoadSystem.SaveBGVolumn(value.newValue);
    }

    private void OnVFXVolumnChangeValueSlider(ChangeEvent<float> value)
    {
        audioSystem.ChangeVFXVolumn(value.newValue);
        SaveLoadSystem.SaveVFXVolumn(value.newValue);
    }   
}
