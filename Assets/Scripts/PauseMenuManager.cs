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

    public Animator animator;
    


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

        if (MenuManager.menuInstance.isSFXMuted == false)
        {
            sfxButtonImg.sprite = OrigImg;
            SfxManager.sfxInstance.audio.mute = false;
        }
        else if (MenuManager.menuInstance.isSFXMuted)
        {
            sfxButtonImg.sprite = MuteImg;
            SfxManager.sfxInstance.audio.mute = true;
            
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
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelection");
    }
    
    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        animator.SetTrigger("Close");
        StartCoroutine(WaitForRetry());

    }
    public void MuteMusic()
    {
        if (MusicManager.musicInstance.audio.mute == false)
        {
            
            musicButtonImg.sprite = MuteImg;
            MusicManager.musicInstance.audio.mute = true;
            
        }
        else if(MusicManager.musicInstance.audio.mute)
        {
            
            musicButtonImg.sprite = OrigImg;
            MusicManager.musicInstance.audio.mute = false;
        }
        
    }
    public void SfxMute()
    {
        if (SfxManager.sfxInstance.audio.mute == false)
        {
           
            sfxButtonImg.sprite = MuteImg;
            SfxManager.sfxInstance.audio.mute = true;

            
        }
        else if(SfxManager.sfxInstance.audio.mute)
        {
            
            sfxButtonImg.sprite = OrigImg;
            SfxManager.sfxInstance.audio.mute = false;
            
        }
        
    }

    IEnumerator WaitForRetry()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
