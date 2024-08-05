using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class SceneScore : MonoBehaviour
{
    public Sprite StarEnable;
    public Sprite StarDisable;

    private int starScore;
    private VisualElement rootStar;

    private StyleBackground starStyleBackgroundEnable;
    private StyleBackground starStyleBackgroundDisable;
    private List<Label> starLabel;

    public int StarScore {get => starScore; set => starScore = value; }

    private void Awake() {
        rootStar = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("star-bar");

        starLabel = new List<Label>();
 
        starLabel = rootStar.Query<Label>().ToList();
    }

    private void Start() {
        starScore = 0;
        starStyleBackgroundEnable = new StyleBackground(StarEnable);
        starStyleBackgroundDisable = new StyleBackground(StarDisable);
    }

    void Update()
    {
        for(int i=0;i<starScore;i++)
        {
            starLabel[i].style.backgroundImage = starStyleBackgroundEnable;  
        }
        for(int i=starScore;i<starLabel.Count;i++)
        {
            starLabel[i].style.backgroundImage = starStyleBackgroundDisable;
        }
    }

    public void UpdateStarScore(int value)
    {
        starScore = value;
    }
}
