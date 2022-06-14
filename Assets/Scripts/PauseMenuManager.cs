using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButton;
    

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuButton.SetActive(true);
        Time.timeScale = 1;
    }
   
    public void LevelSelectionMenuButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
    
}
