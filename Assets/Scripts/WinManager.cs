using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class WinManager : MonoBehaviour
{
    public GameObject WinMenu;
    public GameObject StopWatch;
    public TextMeshProUGUI finalTimeText;
    public float WaitBeforeWin;
    public int nextSceneLoad;
    private int activeScene;

    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        activeScene = SceneManager.GetActiveScene().buildIndex;
    }
    
    private void GetStopWatch()
    {
        StopWatch.GetComponent<StopWatch>().StopWatchActive = false;
        TimeSpan time = StopWatch.GetComponent<StopWatch>().time;
        finalTimeText.text = time.ToString(@"mm\:ss\:fff");
        StopWatch.SetActive(false);
        
        
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetStopWatch();
            StartCoroutine(WaitToWin(WaitBeforeWin));
            
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }

            switch (activeScene)
            {
                case 2 : 
                    PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQAQ");
                    break;
                case 3 : 
                    PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQAg");
                    break;
                case 4 : 
                    PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQAw");
                    break;
                case 5 : 
                    PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBA");
                    break;
                case 6 : 
                    PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQDw");
                    break;
                default:
                    break;
            }
            
        }
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitToWin(float time)
    {
        yield return new WaitForSeconds(time);
        WinMenu.SetActive(true);
    }

}
