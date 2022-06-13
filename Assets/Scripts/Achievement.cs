using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class Achievement : MonoBehaviour
{

    private static Achievement instance;
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
    public void UnlockAchievement(string achievementId)
    {
        Social.ReportProgress(achievementId, 100.0f, (bool success) => { });
    }
}
