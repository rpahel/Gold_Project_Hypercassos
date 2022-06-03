using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuManager : MonoBehaviour
{
    public string levelName;
    
    public void LevelSelectButton()
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            LevelSelectButton();
        }
    }
}
