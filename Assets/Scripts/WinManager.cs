using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class WinManager : MonoBehaviour
{
    public GameObject WinMenu;
    public StopWatch StopWatch;
    public TextMeshProUGUI finalTimeText;
    public float WaitBeforeWin;
    public int nextSceneLoad;
    public Animator animator;
    private int activeScene;
    private Animator level;
    
    

    

    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        activeScene = SceneManager.GetActiveScene().buildIndex;
        
        level = GameObject.Find("Level").GetComponent<Animator>();
        
    }
    
    private void GetStopWatch()
    {
        StopWatch.GetComponent<StopWatch>().StopWatchActive = false;
        TimeSpan time = StopWatch.time;
        finalTimeText.text = time.ToString(@"mm\:ss\:fff");
        

        switch (time.TotalSeconds)
        {
            case < 45 when activeScene == 2:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBQ");
                PlayerPrefs.SetInt("SpeedAchievement", 1); 
                break;
            case < 85  when activeScene == 3:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBg");
                PlayerPrefs.SetInt("SpeedAchievement", 2);
                break;
            case < 90  when activeScene == 4:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBw");
                PlayerPrefs.SetInt("SpeedAchievement", 3);
                break;
            case < 80  when activeScene == 5:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQCA");
                PlayerPrefs.SetInt("SpeedAchievement", 4);
                break;
            case < 75  when activeScene == 6:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQEA");
                PlayerPrefs.SetInt("SpeedAchievement", 5);
                break;
            default:
                    break;
        }
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().speed = 0;
            col.GetComponent<CircleCollider2D>().enabled = false;
            col.GetComponent<Animator>().SetTrigger("End");
            col.GetComponent<PlayerBody>().HideBody();
            level.SetTrigger("Resize");

            if (PlayerPrefs.GetInt("SpeedAchievement") == 5)
            {
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQCQ");
            }
            
            GetStopWatch();
            StartCoroutine(WaitToWin(WaitBeforeWin));

            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                return;
            }
            else
            {
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
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

    public void Retry()
    {
        animator.SetTrigger("Close");
        StartCoroutine(WaitToRetry());
    }

    public void NextLevel()
    {
        animator.SetTrigger("Close");
        StartCoroutine(WaitToNext());
    }

    IEnumerator WaitToWin(float time)
    {
        yield return new WaitForSeconds(time);
        WinMenu.SetActive(true);
    }

    IEnumerator WaitToNext()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(nextSceneLoad);
    }

    IEnumerator WaitToRetry()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
