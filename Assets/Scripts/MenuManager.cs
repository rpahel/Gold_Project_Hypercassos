using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public string levelName;
    [SerializeField]private GameObject optionMenu;
    [SerializeField]private GameObject CloseMenu;
    private bool isMusicMuted;
    private bool isSFXMuted;

    [Header("Music")]
    public Image musicImg;
    
    
    [Header("SFX")]
    public Image sfxImg;
    
    
    [Header("Mute And Original")]
    public Sprite MuteImg;
    public Sprite OrigImg;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SfxManager.sfxInstance.audio.PlayOneShot(SfxManager.sfxInstance.click);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void OptionMenu()
    {
        if (!optionMenu.activeSelf)
        {
            optionMenu.SetActive(true);
        }
    }

    public void CloseOptionMenu()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MuteMusic()
    {
        if (isMusicMuted == false)
        {
            isMusicMuted = true;
            musicImg.sprite = MuteImg;
            MusicManager.musicInstance.audio.mute = true;
            
        }
        else if(isMusicMuted)
        {
            isMusicMuted = false;
            musicImg.sprite = OrigImg;
            MusicManager.musicInstance.audio.mute = false;
        }
        
    }
    
    public void SfxMute()
    {
        if (isSFXMuted == false)
        {
            isSFXMuted = true;
            sfxImg.sprite = MuteImg;
            SfxManager.sfxInstance.audio.mute = true;
        }
        else if(isSFXMuted)
        {
            isSFXMuted = false;
            sfxImg.sprite = OrigImg;
            SfxManager.sfxInstance.audio.mute = false;
        }
        
    }
    
    
}
