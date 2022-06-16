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
    private CameraRotation camera2;

    public Animator animVerEat;
   
    

    

    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        activeScene = SceneManager.GetActiveScene().buildIndex;
        
        level = GameObject.Find("Level").GetComponent<Animator>();
        camera2 = GameObject.Find("CameraController").GetComponent<CameraRotation>();
        
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
                break;
            case < 85  when activeScene == 3:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBg");
                break;
            case < 90  when activeScene == 4:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQBw");
                break;
            case < 80  when activeScene == 5:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQCA");
                break;
            case < 75  when activeScene == 6:
                PlayAchievement.instance.UnlockAchievement("CgkIx4L88PIFEAIQEA");
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
            col.GetComponent<Animator>().enabled = true;
            col.GetComponent<Player>().Freeze();
            col.GetComponent<Animator>().SetTrigger("End");
            col.GetComponent<PlayerBody>().DestroyBody(false);
            level.SetTrigger("Resize");
            StartCoroutine(WaitForStopAnim());

            
            
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

    IEnumerator WaitForStopAnim()
    {
        yield return new WaitForSeconds(0.7f);
        camera2.player = level.gameObject;
        yield return new WaitForSeconds(1f);
        animVerEat.SetTrigger("Go");
        yield return new WaitForSeconds(5f);
        WinMenu.SetActive(true);

    }

}
