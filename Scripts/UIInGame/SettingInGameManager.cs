using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SettingInGameManager : MonoBehaviour
{
    public GameObject PauseMenu;

    private ClickAudio ClickSound;

    private SaveLoadManager saveLoad;

    private void OnEnable() {
        saveLoad = GameObject.FindWithTag("saveload").GetComponent<SaveLoadManager>();

        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("back-to-game-menu").clicked += () => BackToGameMenuAction();
        root.Q<Button>("back-button").clicked += () => BackAction();
    }

    private void BackToGameMenuAction()
    {
        ClickSound.PlayClickSound();
        if(SaveLoadSystem.LoadCompletedScene() == false)
        {
            SaveLoadSystem.DeleteSceneInSaveSlot(SaveLoadSystem.LoadSceneIndex(),SaveLoadSystem.LoadSlotIndex());
        }
        SceneManager.LoadScene("GameMenu");
    }

    private void BackAction()
    {
        ClickSound.PlayClickSound(); 
        PauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
