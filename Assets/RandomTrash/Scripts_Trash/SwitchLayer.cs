using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    private LevelBehaviour level;
    public bool upLayer;
    public bool sameLayerTp;
    private bool cancoli;
    public GameObject tpTarget;
    
    [HideInInspector]public BoxCollider2D boxCollider;
    private void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
        cancoli = true;
        boxCollider = tpTarget.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<ASTITOUCH>().isClone)
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }  
        if (collision.gameObject.tag == "Player"&& !collision.gameObject.GetComponent<ASTITOUCH>().isClone)
        {
            var found = FindObjectsOfType<ASTITOUCH>();

            for (int i = 0; i < found.Length; i++)
            {
                if (found[i].GetComponent<ASTITOUCH>().isClone)
                {
                    Destroy(found[i].transform.parent.gameObject);
                }
            }
            if (tpTarget!=null)
            {

                if (tpTarget.GetComponent<SwitchLayer>() != null)
                {
                    StartCoroutine(coliTimer());
                }

                collision.gameObject.transform.position = tpTarget.transform.position;
                
                Debug.Log("Tp");
               
            }
            if (upLayer && !sameLayerTp)
            {
                level.RequestLayerUp();
                StartCoroutine(coliTimer());
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000, ForceMode2D.Impulse);
            }
            else if (sameLayerTp)
            {
                
                StartCoroutine(coliTimer());
                collision.gameObject.transform.position = tpTarget.transform.position;                
            }
            else
            {
                level.RequestLayerDown();
                StartCoroutine(coliTimer());
            }
        }
    }

    public IEnumerator coliTimer()
    {
        Debug.Log("dans la couroutine");
        //cancoli = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
        cancoli = true;
    }
}
