using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class SettingMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject AudioMenu;
    [SerializeField] private GameObject KeyboardSettingMenu;
    [SerializeField] private GameObject GameMenu;

    private ClickAudio ClickSound;
    
    private void OnEnable() {
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("keyboard-button").clicked += () => KeyboardSettingAction();
        root.Q<Button>("audio-button").clicked += () => AudioAction();
        root.Q<Button>("back-button").clicked += () => BackAction();
    }

    private void KeyboardSettingAction()
    {
        ClickSound.PlayClickSound();
        gameObject.SetActive(false);
        KeyboardSettingMenu.SetActive(true);
    }

    private void AudioAction()
    {
        ClickSound.PlayClickSound();
        gameObject.SetActive(false);
        AudioMenu.SetActive(true);
    }

    private void BackAction()
    {
        ClickSound.PlayClickSound();
        gameObject.SetActive(false);
        GameMenu.SetActive(true);
    }
    
}
