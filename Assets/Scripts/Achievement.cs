using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class Achievement : MonoBehaviour
{

    public static Achievement instance;
    public int numberOfLayer;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
    }
    private void Update()
    {
        if(numberOfLayer == 30)
        {
            UnlockAchievement("202637246791");
        }
    }
    public void UnlockAchievement(string achievementId)
    {
        Social.ReportProgress(achievementId, 100.0f, (bool success) => { });
    }
}
