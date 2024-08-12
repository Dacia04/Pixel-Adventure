using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ReplayLevelManager : MonoBehaviour
{  
    private ClickAudio ClickSound;


    private void OnEnable()
    {    

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        root.Q<Label>("Notice-Label").text = "You've archive "+ 
                SaveLoadSystem.LoadScoreSystem()[SaveLoadSystem.LoadSceneIndex()]+
                " star at this level.\nDo you want to play again?";
        root.Q<Button>("Yes-Button").clicked += () => YesAction();
        root.Q<Button>("No-Button").clicked += () => NoAction();
    }

    private void YesAction()
    {
        SaveLoadSystem.DeleteSceneInSaveSlot(SaveLoadSystem.LoadSceneIndex(),SaveLoadSystem.LoadSlotIndex());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SaveLoadSystem.LoadSceneIndex() );
    }

    private void NoAction()
    {
        gameObject.SetActive(false);
    }


}
