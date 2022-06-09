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
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().IsClone)
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
    
}
