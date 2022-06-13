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
            var oui = FindObjectsOfType<ASTITOUCH>();

            for (int i = 0; i < oui.Length; i++)
            {
                if (oui[i].GetComponent<ASTITOUCH>().isClone)
                {
                    Destroy(oui[i].transform.parent.gameObject);
                }
            }
        }
        
    }
    
}
