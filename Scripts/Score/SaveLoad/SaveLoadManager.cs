using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{

    private SceneScore sceneScore;

    private List<int> ScoreList;
    private List<int> StarList;

    private List<GameObject> StarObjectList;

    private GameObject player;

    private int saveSlot ;

    private void Start()
    {
        SaveLoadSystem.SaveSceneIndex(SceneManager.GetActiveScene().buildIndex);
        
        saveSlot = SaveLoadSystem.LoadSlotIndex();

        ScoreList = new List<int>();
        ScoreList = LoadScoreGame();

        sceneScore = GameObject.FindWithTag("UI").GetComponent<SceneScore>();
        sceneScore.StarScore = ScoreList[SceneManager.GetActiveScene().buildIndex];

        player = GameObject.FindWithTag("Player");

        StarList = new List<int>();
        StarList = LoadStarGame();

        StarObjectList = new List<GameObject>();
        EnableStarObjectInScene();

        if(!SaveLoadSystem.LoadCompletedScene())
        {
            if(SaveLoadSystem.LoadSavePointPosition() != new Vector2(0,0))
            {
                player.transform.position = SaveLoadSystem.LoadSavePointPosition();
            }           
        }

        Debug.Log("SaveSlot:" + saveSlot +" scene:"+ SceneManager.GetActiveScene().buildIndex +" score:" + sceneScore.StarScore + " " +
                  "Score: "+ ScoreList[SceneManager.GetActiveScene().buildIndex] + " " +
                   "Star: " + StarList[0] +","+ StarList[1] +","+ StarList[2] +" " +
                   "State: " + SaveLoadSystem.LoadCompletedScene());
    }

    
    private void Update()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        ScoreList[index]= sceneScore.StarScore;
        UpdateStateStar();
    }
 
    #region Save Load Region
    public void SaveGameCompleted()
    {
        SaveLoadSystem.SaveSystem(ScoreList,StarList,(Vector2)player.transform.position,true);
    }
    public void SaveGameUncompleted()
    {
        SaveLoadSystem.SaveSystem(ScoreList,StarList,(Vector2)player.transform.position,false);
    }
    public List<int> LoadScoreGame()
    {
        return SaveLoadSystem.LoadScoreSystem();
    }
    public List<int> LoadStarGame()
    {
        return SaveLoadSystem.LoadStarSystem();
    }
    #endregion


    #region other function
    public void EnableStarObjectInScene()
    {
        GameObject[] starObject = GameObject.FindGameObjectsWithTag("Score");
        foreach(GameObject item in starObject)
        {
            StarObjectList.Add(item);
        }

        StarObjectList = StarObjectList.OrderBy(x => x.transform.position.x).ToList();

        int size = StarList.Count;
        for(int i=0;i<size;i++)
        {
            if(StarList[i]==1)
            {
                StarObjectList[i].SetActive(true);
            }
            else
            {
                StarObjectList[i].SetActive(false);
            }
        }
    }

    public void UpdateStateStar()
    {
        int size = StarObjectList.Count;
        for(int i=0;i<size;i++)
        {
            if(StarObjectList[i].activeSelf)
            {
                StarList[i] =1;
            }
            else
            {
                StarList[i]=0;
            }
        }
    }

    #endregion

}
