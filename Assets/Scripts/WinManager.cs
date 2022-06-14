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
    public int nextSceneLoad;

    private void GetStopWatch()
    {
        StopWatch.GetComponent<StopWatch>().StopWatchActive = false;
        TimeSpan time = StopWatch.GetComponent<StopWatch>().time;
        finalTimeText.text = time.ToString(@"mm\:ss\:fff");
        StopWatch.SetActive(false);
    
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetStopWatch();
            WinMenu.SetActive(true);
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
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
    
}
