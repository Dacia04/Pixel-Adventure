using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveMenuManager : MonoBehaviour
{
    [SerializeField] GameObject GameMenu;
    [SerializeField] GameObject SceneMenu;
    
    private ClickAudio ClickSound;

    private void OnEnable()
    {    
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("save-slot-1-button").clicked += () => Save1Action();
        root.Q<Button>("save-slot-2-button").clicked += () => Save2Action();
        root.Q<Button>("save-slot-3-button").clicked += () => Save3Action();

        root.Q<Button>("delete-slot-1").clicked += () => DeleteSlot(1);
        root.Q<Button>("delete-slot-2").clicked += () => DeleteSlot(2);
        root.Q<Button>("delete-slot-3").clicked += () => DeleteSlot(3);

        root.Q<Button>("back-button").clicked +=() => BackAction();
    }

    private void Save1Action()
    {
        SaveLoadSystem.SaveSlotIndex(1);

        ClickSound.PlayClickSound();
        SceneMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Save2Action()
    {
        SaveLoadSystem.SaveSlotIndex(2);

        ClickSound.PlayClickSound();
        SceneMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Save3Action()
    {
        SaveLoadSystem.SaveSlotIndex(3);

        ClickSound.PlayClickSound();
        SceneMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void BackAction()
    {
        ClickSound.PlayClickSound();
        GameMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void DeleteSlot(int index)
    {
        SaveLoadSystem.DeleteSaveSlot(index);
    }
}
