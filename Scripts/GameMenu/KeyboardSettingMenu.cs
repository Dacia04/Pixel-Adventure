using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardSettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenu;

    private ClickAudio ClickSound;
    
    private void OnEnable() {
        ClickSound = GameObject.FindWithTag("ClickSound").GetComponent<ClickAudio>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("back-button").clicked += () => BackAction();
    }

    private void BackAction()
    {
        ClickSound.PlayClickSound();
        gameObject.SetActive(false);
        SettingMenu.SetActive(true);
    }
}
