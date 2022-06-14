using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelName;
    [SerializeField]private GameObject optionMenu;
    [SerializeField]private GameObject CloseMenu;

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
}
