using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject SettingInGame;
    public GameObject UiInGame;

    private ClickAudio ClickSound;


    private void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        UiInGame.SetActive(false);

        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        root.Q<Button>("setting-button").clicked += () => SettingAction();
        root.Q<Button>("resume-button").clicked += () => ResumeAction();
        root.Q<Button>("replay-button").clicked += () => ReplayAction();

    }

    private void SettingAction()
    {
        ClickSound.PlayClickSound();
        SettingInGame.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ResumeAction()
    {
        ClickSound.PlayClickSound();
        UiInGame.SetActive(true);
        UIInGameManager.instance.Continue();
    }

    private void ReplayAction()
    {
        ClickSound.PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
