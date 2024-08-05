using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    private int currentHealth;
    private int preHealth;
    private Label heart;
    private VisualElement rootHealth;

    private List<Label> heartLabels;   
    private void Awake()
    {
        rootHealth = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("health-bar");

        heartLabels = new List<Label>();

        heartLabels = rootHealth.Query<Label>().ToList();
    }

    private void Start() {
        currentHealth = _player.playerData.Health;
        preHealth = _player.playerData.Health;

        for(int i= currentHealth;i< heartLabels.Count;i++)
        {
            rootHealth.Remove(heartLabels[i]);
        }

    }

    void Update()
    {
        preHealth = currentHealth;
        currentHealth = _player.CurrentHealth;

        if(currentHealth>=0)
        {
            if(currentHealth < preHealth)
            {
                for(int i=currentHealth;i<preHealth;i++)
                {
                    //heartLabels[i].SetEnabled(false);
                    heartLabels[i].style.opacity = 0f;
                }
            }
            else if(currentHealth > preHealth)
            {
                for(int i=preHealth;i<currentHealth;i++)
                {
                    //heartLabels[i].SetEnabled(true);
                    heartLabels[i].style.opacity = 100f;
                }
            }
        }
        

    }
}
