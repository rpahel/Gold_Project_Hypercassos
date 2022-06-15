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
    [HideInInspector] public bool isSFXMuted;

    [Header("Music")]
    public Image musicImg;
    
    
    [Header("SFX")]
    public Image sfxImg;
    
    
    [Header("Mute And Original")]
    public Sprite MuteImg;
    public Sprite OrigImg;

    public static MenuManager menuInstance;

    private void Awake()
    {
        if (menuInstance != null && menuInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        menuInstance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
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
        if (MusicManager.musicInstance.audio.mute == false)
        {
           
            musicImg.sprite = MuteImg;
            MusicManager.musicInstance.audio.mute = true;
            
        }
        else if(MusicManager.musicInstance.audio.mute)
        {
            
            musicImg.sprite = OrigImg;
            MusicManager.musicInstance.audio.mute = false;
        }
        
    }
    
    public void SfxMute()
    {
        if (SfxManager.sfxInstance.audio.mute == false)
        {
            isSFXMuted = true;
            sfxImg.sprite = MuteImg;
            SfxManager.sfxInstance.audio.mute = true;
        }
        else if(SfxManager.sfxInstance.audio.mute)
        {
            isSFXMuted = false;
            sfxImg.sprite = OrigImg;
            SfxManager.sfxInstance.audio.mute = false;
        }
        
    }
    
    
}
