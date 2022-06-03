using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelButton : MonoBehaviour
{
    private int levelNum;
    public TextMeshProUGUI buttonText;  
    private string levelNumStr; 
   

    void Start()
    {
        levelNumStr = buttonText.text.Remove(0, 5);
        levelNum = int.Parse(levelNumStr);
    }

    public void LoadLevel()
    {
        //Jme permets pour la sortie proto hein des bisous
        //Debug.Log(levelNum);
        //SceneManager.LoadScene("Level " + levelNum); 

        SceneManager.LoadScene("Raphael");
    }
}
