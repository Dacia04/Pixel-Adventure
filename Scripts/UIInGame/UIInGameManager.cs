using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInGameManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Player _player;

    private ClickAudio ClickSound;
    public static UIInGameManager instance;

    private Button setting_button;

    VisualElement root;

    private void OnEnable() {
        instance=this;
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        root = GetComponent<UIDocument>().rootVisualElement;

         setting_button = root.Q<Button>("setting-button");

         setting_button.clicked += () => SettingAction();
    }

    private void SettingAction()
    {
        ClickSound.PlayClickSound();
        Time.timeScale =0f;
        _player.InputHandler.DisableControl();
        PauseMenu.SetActive(true);
        setting_button.SetEnabled(false);
    }

    public void Continue()
    {
        Time.timeScale=1f;
        _player.InputHandler.EnableControl();
        PauseMenu.SetActive(false);
        setting_button.SetEnabled(true);
        
    } 
}
