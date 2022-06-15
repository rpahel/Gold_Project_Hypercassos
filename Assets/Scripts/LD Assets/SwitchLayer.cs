using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLayer : MonoBehaviour
{
    
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Tooltip("Put an object here only if you want the player to teleport somewhere precise."), SerializeField]
    private Transform LayerTpTarget;
    [Tooltip("Put an object here only if you want the player to teleport somewhere precise."), SerializeField, Range(0f, 2f)]
    private float TpTime;
    [Tooltip("True if you want this to be the level ending."), SerializeField]
    private bool isEnding;
    [Tooltip("Choose if this tp takes you up, down, or on the same layer (none)."), SerializeField]
    private Where upOrDown;

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
            if (collision.GetComponent<Player>().isClone)
            {
                collision.GetComponent<BodyBehaviour>().DestroyBodyCircle();
            }
            if (isEnding)
            {
                SceneManager.LoadScene("LevelSelection");
                return;
            }

            if (LayerTpTarget)
            {
                Vector3 target = new Vector3(LayerTpTarget.position.x, LayerTpTarget.position.y, collision.transform.position.z);
                collision.GetComponent<Player>().SmoothGoTo(target, TpTime);
            }

            if (upOrDown == Where.UP)
            {
                level.RequestLayerUp();
                StartCoroutine(colliTimer());
                PlayAchievement.instance.UnlockAchievement("202637246791");
            }
            else if (upOrDown == Where.DOWN)
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

enum Where
{
    UP = 0,
    DOWN = 1,
    NONE = 2
}
