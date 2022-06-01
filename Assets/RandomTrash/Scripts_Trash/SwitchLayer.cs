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
        
        if (collision.gameObject.tag == "Player" && cancoli)
        {
            if (upLayer)
            {
                level.RequestLayerUp();
                StartCoroutine(coliTimer());
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000,ForceMode2D.Impulse);
            }
            else
            {
                level.RequestLayerDown();
                StartCoroutine(coliTimer());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && cancoli)
        {

                level.RequestLayerDown();
                StartCoroutine(coliTimer());
            collision.gameObject.transform.up *= 20;
        }
    }
    IEnumerator coliTimer()
    {
        cancoli = false;
        yield return new WaitForSeconds(1f);
        cancoli = true;
    }
}
