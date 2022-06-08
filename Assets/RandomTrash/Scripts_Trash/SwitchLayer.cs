using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    private LevelBehaviour level;
    public bool upLayer;
    private bool cancoli;
    public GameObject tpTarget;
    private void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
        cancoli = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && cancoli)
        {

            if(tpTarget!=null)
            {

                if (tpTarget.GetComponent<SwitchLayer>() != null)
                {
                    tpTarget.GetComponent<SwitchLayer>().StartCoroutine(coliTimer());
                }


                collision.gameObject.transform.position = tpTarget.transform.position;
                
                Debug.Log("Tp");
               
            }
            if (upLayer)
            {
                level.RequestLayerUp();
                StartCoroutine(coliTimer());
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000, ForceMode2D.Impulse);
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
