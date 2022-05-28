using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    public LevelBehaviour level;
    public bool upLayer;
    public bool cancoli;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coli");
        if (collision.gameObject.tag == "Player" && cancoli)
        {
            if (upLayer)
            {
                StartCoroutine(level.LayerUp());
                StartCoroutine(coliTimer());
            }
            else
            {
                StartCoroutine(level.LayerDown());
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
