using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiClonage : MonoBehaviour
{
    public Transform target;
    public GameObject playerPrefabs;
    private GameObject playerClone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
        if(playerClone == null)
        {
            playerClone = Instantiate(playerPrefabs, target.position,Quaternion.identity);
        }
        }

    }
}
