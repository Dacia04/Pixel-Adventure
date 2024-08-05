using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject GameMenu;
    [SerializeField] private GameObject ReplayMenu;

    private ClickAudio ClickSound;

    private SaveLoadManager saveLoad;

    void OnEnable()
    {      
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("level-1-button").clicked += () => Level1Action();
        root.Q<Button>("level-2-button").clicked += () => Level2Action();
        root.Q<Button>("level-3-button").clicked += () => Level3Action();
        root.Q<Button>("back-button").clicked += () => BackAction();
    }

    private void Level1Action()
    {
        ClickSound.PlayClickSound();
        SaveLoadSystem.SaveSceneIndex(1);
        if(SaveLoadSystem.LoadCompletedScene())
        {
            ReplayMenu.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SaveLoadSystem.LoadSceneIndex() );
        }
             
        Time.timeScale=1f;
    }

    private void Level2Action()
    {
        ClickSound.PlayClickSound();
        SaveLoadSystem.SaveSceneIndex(2);
        if(SaveLoadSystem.LoadCompletedScene())
        {
            ReplayMenu.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SaveLoadSystem.LoadSceneIndex() );
        }
        Time.timeScale=1f;
    }
    private void Level3Action()
    {
        ClickSound.PlayClickSound();
        SaveLoadSystem.SaveSceneIndex(3);
        if(SaveLoadSystem.LoadCompletedScene())
        {
            ReplayMenu.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SaveLoadSystem.LoadSceneIndex() );
        } 
        Time.timeScale=1f;
    }
    private void BackAction()
    {
        ClickSound.PlayClickSound();
        GameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
