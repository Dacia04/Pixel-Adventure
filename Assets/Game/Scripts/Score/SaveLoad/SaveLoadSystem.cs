using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadSystem
{
    public static void SaveSystem(List<int> scoreList,List<int> starList,Vector2 positon,bool completed)
    {
        // save score;
        int size1 = scoreList.Count;
        for(int i=0;i<size1;i++)
        {
            PlayerPrefs.SetInt( CreateNameVarialeScore(i,LoadSlotIndex()) , scoreList[i] );
        }

        //save star
        int size2 = starList.Count;
        int sceneIndex= SceneManager.GetActiveScene().buildIndex;

        PlayerPrefs.SetInt(CreateNameVariableNumberOfStar(sceneIndex,LoadSlotIndex()),starList.Count);
        for(int i=0;i<size2;i++)
        {
            PlayerPrefs.SetInt(  CreateNameVariableStar(sceneIndex,i,LoadSlotIndex())  ,   starList[i]  );
        }

        // save point;
        PlayerPrefs.SetFloat(CreateNameVariablePositionXOfPlayer(sceneIndex,LoadSlotIndex()),positon.x);
        PlayerPrefs.SetFloat(CreateNameVariablePositionYOfPlayer(sceneIndex,LoadSlotIndex()),positon.y);
        
        if(completed)
        {
            PlayerPrefs.SetInt(CreateNameVariableCompleted(sceneIndex,LoadSlotIndex()),1);
        }
        else
        {
            PlayerPrefs.SetInt(CreateNameVariableCompleted(sceneIndex,LoadSlotIndex()),0);
        }
    }

    public static List<int> LoadScoreSystem()
    {
        List<int> scoreList = new List<int>();
        int size = SceneManager.sceneCountInBuildSettings;
        for(int i=0;i<size;i++)
        {
            scoreList.Add(0);
        }

        for(int i=0;i<size;i++)
        {
            if(PlayerPrefs.HasKey(  CreateNameVarialeScore(i,LoadSlotIndex())  )  )
                scoreList[i] = PlayerPrefs.GetInt( CreateNameVarialeScore(i,LoadSlotIndex()) ); 
        }

        return scoreList;
    }

    public static List<int> LoadStarSystem()
    {
        List<int> starList = new List<int>();
        int size;
        if(GameObject.FindWithTag("Score") != null)
        {
            size = GameObject.FindGameObjectsWithTag("Score").Length;
        }
        else
            size = 3;
        
        for(int i=0;i<size;i++)
        {
            starList.Add(0);
        }

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        for(int i=0;i<size;i++)
        {
            if(PlayerPrefs.HasKey(CreateNameVariableStar(sceneIndex,i,LoadSlotIndex())))
            {
                starList[i] = PlayerPrefs.GetInt(CreateNameVariableStar(sceneIndex,i,LoadSlotIndex()));
            }
            else
            {
                starList[i] = 1;
            }
        }

        return starList;
    }

    public static void SaveSlotIndex(int index)
    {
        PlayerPrefs.SetInt("ThisSaveIndex",index);
    }

    public static int LoadSlotIndex()
    {
        return PlayerPrefs.GetInt("ThisSaveIndex");
    }

    public static void SaveSceneIndex(int index)
    {
        PlayerPrefs.SetInt("ThisSceneIndex",index);
    }
    public static int LoadSceneIndex()
    {
        return PlayerPrefs.GetInt("ThisSceneIndex");
    }

    public static Vector2 LoadSavePointPosition()
    {
        int sceneIndex= SceneManager.GetActiveScene().buildIndex;
        Vector2 temp = new Vector2();
        temp.x = PlayerPrefs.GetFloat(CreateNameVariablePositionXOfPlayer(sceneIndex,LoadSlotIndex()));
        temp.y = PlayerPrefs.GetFloat(CreateNameVariablePositionYOfPlayer(sceneIndex,LoadSlotIndex()));
        return temp;
    }

    public static bool LoadCompletedScene()
    {
        int sceneIndex = LoadSceneIndex();
        if(PlayerPrefs.GetInt(CreateNameVariableCompleted(sceneIndex,LoadSlotIndex())) ==1)
        {
            return true;
        }
        return false;
    }

    public static void SaveMasVolumn(float value)
    {
        //Debug.Log("Save");
        PlayerPrefs.SetFloat(CreateNameVariableMasVolumn(),value);
    }
    public static void SaveBGVolumn(float value)
    {
        PlayerPrefs.SetFloat(CreateNameVariableBGVolumn(),value);
    }
    public static void SaveVFXVolumn(float value)
    {
        PlayerPrefs.SetFloat(CreateNameVariableVFXVolumn(),value);
    }
    public static List<float> LoadVolumn()
    {   
        List<float> vol = new List<float>() {50f,50f,50f};
        if(PlayerPrefs.HasKey(CreateNameVariableMasVolumn()))
            vol[0] = PlayerPrefs.GetFloat(CreateNameVariableMasVolumn());
        if(PlayerPrefs.HasKey(CreateNameVariableBGVolumn()))
            vol[1] = PlayerPrefs.GetFloat(CreateNameVariableBGVolumn());
        if(PlayerPrefs.HasKey(CreateNameVariableVFXVolumn()))
            vol[2] = PlayerPrefs.GetFloat(CreateNameVariableVFXVolumn());
        return vol;
    }

    #region Delete data
    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeleteSaveSlot(int saveSlotIndex)
    {
        int size = SceneManager.sceneCountInBuildSettings;

        for(int i=0;i<size;i++)
        {
            PlayerPrefs.DeleteKey(CreateNameVarialeScore(i,saveSlotIndex));

            int starCount= PlayerPrefs.GetInt(CreateNameVariableNumberOfStar(i,saveSlotIndex));
            for(int j=0;j< starCount;j++)
            {
                PlayerPrefs.DeleteKey(CreateNameVariableStar(i,j,saveSlotIndex));
            }

            PlayerPrefs.DeleteKey(CreateNameVariableCompleted(i,saveSlotIndex));
            PlayerPrefs.DeleteKey(CreateNameVariablePositionXOfPlayer(i,saveSlotIndex));
            PlayerPrefs.DeleteKey(CreateNameVariablePositionYOfPlayer(i,saveSlotIndex));
            
        }
    }

    public static void DeleteSceneInSaveSlot(int sceneIndex,int saveSlotIndex)
    {
        PlayerPrefs.DeleteKey(CreateNameVarialeScore(sceneIndex,saveSlotIndex));
        int starCount= PlayerPrefs.GetInt(CreateNameVariableNumberOfStar(sceneIndex,saveSlotIndex));
        for(int j=0;j< starCount;j++)
        {
            PlayerPrefs.DeleteKey(CreateNameVariableStar(sceneIndex,j,saveSlotIndex));
        }

        PlayerPrefs.DeleteKey(CreateNameVariableCompleted(sceneIndex,saveSlotIndex));
        PlayerPrefs.DeleteKey(CreateNameVariablePositionXOfPlayer(sceneIndex,saveSlotIndex));
        PlayerPrefs.DeleteKey(CreateNameVariablePositionYOfPlayer(sceneIndex,saveSlotIndex));

    }
    #endregion

    #region Create Name Key
    private static string CreateNameVarialeScore(int listIndex,int saveSlotIndex)
    {
        return "score" + listIndex.ToString() +" " + saveSlotIndex.ToString();
    }

    private static string CreateNameVariableStar(int sceneIndex,int listIndex,int saveSlotIndex)
    {
        return "star" + sceneIndex.ToString() +" " + listIndex.ToString() + " " + saveSlotIndex.ToString();
    }
    
    private static string CreateNameVariableNumberOfStar(int sceneIndex,int saveSlotIndex)
    {
        return "star count" + sceneIndex.ToString() +" " + saveSlotIndex; 
    }

    private static string CreateNameVariableCompleted(int sceneIndex,int saveSlotIndex)
    {
        return "completed" + sceneIndex.ToString() + " " + saveSlotIndex.ToString(); 
    }

    private static string CreateNameVariablePositionXOfPlayer(int sceneIndex,int saveSlotIndex)
    {
        return "PosX " + sceneIndex.ToString() +" " + saveSlotIndex.ToString(); 
    }

    private static string CreateNameVariablePositionYOfPlayer(int sceneIndex,int saveSlotIndex)
    {
        return "PosY " + sceneIndex.ToString() +" " + saveSlotIndex.ToString(); 
    }
    
    private static string CreateNameVariableMasVolumn()
    {
        return "MasVolumn";
    }
    private static string CreateNameVariableBGVolumn()
    {
        return "BGVolumn";
    }
    private static string CreateNameVariableVFXVolumn()
    {
        return "VFXVolumn";
    }
    #endregion
}