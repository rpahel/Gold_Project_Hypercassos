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
    public bool destroyBox = false;
    public bool destroyPlayer = false;
    private void Awake()
    {
        levelObject = FindObjectOfType<LevelBehaviour>().gameObject;
        level = levelObject.GetComponent<LevelBehaviour>();
        layerCount = level.CurrentLayer + 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            layerCount = level.CurrentLayer + 1;
            
            if (level.CurrentLayer <= level.LevelLayers.Count)
            {
                level.LevelLayers[layerCount].Discover();
            }
            if (playerClone == null)
            {
                playerClone = Instantiate(playerPrefabs, target.position,Quaternion.identity);
                playerClone.GetComponent<Player>().speed +=5;
                playerClone.GetComponent<Player>().isClone=true;
                playerClone.name = "AstiClone";
                PlayAchievement.instance.UnlockAchievement("202637246791");
            }
            else
            {
                if (destroyPlayer)
                    playerClone.GetComponent<PlayerBody>().DestroyBody();
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
                if (destroyBox)
                    Destroy(boxClone.transform.gameObject);
            }
            Debug.Log("Box trigger");
        }
    }
}
