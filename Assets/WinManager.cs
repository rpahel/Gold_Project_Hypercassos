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
            WinMenu.SetActive(true);
            
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
