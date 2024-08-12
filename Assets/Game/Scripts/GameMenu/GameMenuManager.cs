using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject SetttingMenu;
    [SerializeField] GameObject SaveMenu;
    private ClickAudio ClickSound;

    void Start()
    {
        AudioSystem audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();
        audioSystem.LoadVolumn();
    }

    private void OnEnable()
    {    

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        root.Q<Button>("start-button").clicked += () => StartAction();
        root.Q<Button>("setting-button").clicked += () => SettingAction();
        root.Q<Button>("quit-button").clicked += () => QuitAction();

        
    }


    private void StartAction()
    {
        ClickSound.PlayClickSound();
        SaveMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void SettingAction()
    {
        ClickSound.PlayClickSound();
        SetttingMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void QuitAction()
    {
        ClickSound.PlayClickSound();
        Application.Quit();
    }
}
