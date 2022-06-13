using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillClone : MonoBehaviour
{
    
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var found = FindObjectsOfType<ASTITOUCH>();

            for (int i = 0; i < found.Length; i++)
            {
                if (found[i].GetComponent<ASTITOUCH>().isClone)
                {
                    Destroy(found[i].transform.parent.gameObject);
                }
            }
        }
        
    }
    
}
