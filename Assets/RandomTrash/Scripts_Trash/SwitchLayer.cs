using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLayer : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Tooltip("True if you want this to be the level ending."), SerializeField]
    private bool isEnding;
    [Tooltip("True if you want this switch to take you up, False for down."), SerializeField]
    private bool goUp;

    private bool canCollide;
    private LevelBehaviour level;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
        canCollide = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canCollide)
        {
            if (isEnding)
            {
                SceneManager.LoadScene("LevelSelector");
            }

            if (goUp)
            {
                level.RequestLayerUp();
                StartCoroutine(colliTimer());
            }
            else if (sameLayerTp)
            {
                
                StartCoroutine(coliTimer());
                collision.gameObject.transform.position = tpTarget.transform.position;                
            }
            else
            {
                level.RequestLayerDown();
                StartCoroutine(colliTimer());
            }
        }
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    IEnumerator colliTimer()
    {
        canCollide = false;
        yield return new WaitForSeconds(1f);
        canCollide = true;
        yield break;
    }
}
