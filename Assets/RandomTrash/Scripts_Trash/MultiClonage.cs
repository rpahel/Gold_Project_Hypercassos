using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiClonage : MonoBehaviour
{
    public Transform target;
    public Transform targetBox;
    public GameObject playerPrefabs;
    public GameObject boxPrefabs;
    
    private GameObject playerClone;
    private GameObject boxClone;
    public int layerCount;
    private GameObject levelObject;
    private LevelBehaviour level;
    private void Awake()
    {
        levelObject = FindObjectOfType<LevelBehaviour>().gameObject;
        level = levelObject.GetComponent<LevelBehaviour>();
        layerCount = level.currentLayer + 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            layerCount = level.currentLayer + 1;
            level.levelLayers[layerCount].Discover();
            if (playerClone == null)
            {
                playerClone = Instantiate(playerPrefabs, target.position,Quaternion.identity);
                playerClone.transform.GetChild(0).GetComponent<ASTITOUCH>().speed +=5;
                playerClone.transform.GetChild(0).GetComponent<ASTITOUCH>().isClone=true;
            }
            else
            {
                Destroy(playerClone.transform.gameObject);
            }
        }
        else if(collision.CompareTag("Box"))
        {
            if (boxClone == null)
            {
                boxClone = Instantiate(boxPrefabs, targetBox.position, Quaternion.identity);
                Debug.Log("BocClone");
            }
            else
            {
                Destroy(boxClone.transform.gameObject);
            }
            Debug.Log("Box trigger");
        }
    }
}
