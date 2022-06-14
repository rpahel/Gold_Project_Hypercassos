using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButton;
    
    [Header("Music")]
    public Image musicButtonImg;
    
    
    [Header("SFX")]
    public Image sfxButtonImg;
    
    
    [Header("Mute And Original")]
    public Sprite MuteImg;
    public Sprite OrigImg;


    private void Start()
    {
        if (MusicManager.musicInstance.audio.mute == false)
        {
            musicButtonImg.sprite = OrigImg;
        }
        else if(MusicManager.musicInstance.audio.mute)
        {
            
            musicButtonImg.sprite = MuteImg;
        }

        if (SfxManager.sfxInstance.audio.mute == false)
        {
            sfxButtonImg.sprite = OrigImg;
        }
        else if (SfxManager.sfxInstance.audio.mute)
        {
            sfxButtonImg.sprite = MuteImg;
            
        }
    }
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
    public void MuteMusic()
    {
        if (MusicManager.musicInstance.audio.mute == false)
        {
            MenuManager.menuInstance.isMusicMuted = true;
            musicButtonImg.sprite = MuteImg;
            MusicManager.musicInstance.audio.mute = true;
            
        }
        else if(MusicManager.musicInstance.audio.mute)
        {
            MenuManager.menuInstance.isMusicMuted = false;
            musicButtonImg.sprite = OrigImg;
            MusicManager.musicInstance.audio.mute = false;
        }
        
    }
    public void SfxMute()
    {
        if (SfxManager.sfxInstance.audio.mute == false)
        {
            MenuManager.menuInstance.isSFXMuted = true;
            sfxButtonImg.sprite = MuteImg;
            SfxManager.sfxInstance.audio.mute = true;
        }
        else if(SfxManager.sfxInstance.audio.mute)
        {
            MenuManager.menuInstance.isSFXMuted = false;
            sfxButtonImg.sprite = OrigImg;
            SfxManager.sfxInstance.audio.mute = false;
        }
        
    }
    
}
