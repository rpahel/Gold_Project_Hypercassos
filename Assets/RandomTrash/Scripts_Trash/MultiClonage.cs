using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiClonage : MonoBehaviour
{
    public Transform target;
    public GameObject playerPrefabs;
    public GameObject boxPrefabs;

    private GameObject playerClone;
    private GameObject boxClone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerClone == null)
            {
                playerClone = Instantiate(playerPrefabs, target.position,Quaternion.identity);
                playerClone.transform.GetChild(0).GetComponent<ASTITOUCH>().speed +=5;
                playerClone.transform.GetChild(0).GetComponent<ASTITOUCH>().isClone=true;
            }
            else
            {
                Destroy(playerClone);
            }
        }
        else if(collision.CompareTag("Box"))
        {
            if (boxClone == null)
            {
                boxClone = Instantiate(boxPrefabs, target.position, Quaternion.identity);
                Debug.Log("BocClone");
            }
            Debug.Log("Box trigger");
        }
    }
}
