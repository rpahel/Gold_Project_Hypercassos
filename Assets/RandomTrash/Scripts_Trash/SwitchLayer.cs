using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    private LevelBehaviour level;
    public bool upLayer;
    private bool cancoli;

    private void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
        cancoli = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coli");
        if (collision.gameObject.tag == "Player" && cancoli)
        {
            if (upLayer)
            {
                level.RequestLayerUp();
                StartCoroutine(coliTimer());
            }
            else
            {
                level.RequestLayerDown();
                StartCoroutine(coliTimer());
            }
        }
    }
    IEnumerator coliTimer()
    {
        cancoli = false;
        yield return new WaitForSeconds(1f);
        cancoli = true;
    }
}
