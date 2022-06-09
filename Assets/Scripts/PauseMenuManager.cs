using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    
    public GameObject pauseMenuUI;

    public void SetPauseMenu()
    {
        Paused();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
   
    public void LevelSelectionMenuButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    

    public void Quit()
    {
        Application.Quit();
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    
    
    
}
