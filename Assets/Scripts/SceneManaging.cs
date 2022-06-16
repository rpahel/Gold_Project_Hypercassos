using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    private int index;
    public Animator animator;
    public void ChangeScene(int sceneIndex)
    {
        index = sceneIndex;
        animator.SetTrigger("Close");
        StartCoroutine(WaitForLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForLevel()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(index);
    }
}
