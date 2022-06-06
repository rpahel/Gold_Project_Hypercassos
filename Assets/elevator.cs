using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField]private Transform pos1;                                 
    [SerializeField]private Transform pos2;                                 
    private bool retour;                                            
    
    void Update()
    {
        StartCoroutine(Move());

    }

    
    void OnCollisionEnter2D(Collision2D truc) {
        if (truc.gameObject.tag == "Player") {
            truc.transform.parent = transform;
        }
    }

    
    void OnCollisionExit2D(Collision2D truc) {
        if (truc.gameObject.tag == "Player") {
            truc.transform.parent = null;
            truc.transform.parent.localScale = new Vector3(1,1,1);
        }
    }


    IEnumerator Move()
    {
        if (!retour) {
            
            transform.position = Vector2.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);

            if (Vector2.Distance (transform.position, pos1.position) < 0.05f)
            {
                yield return new WaitForSeconds(3f);
                retour = true;
            }
        }
        
        if (retour) {
            transform.position = Vector2.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos2.position) < 0.05f) 
            {
                yield return new WaitForSeconds(3f);
                retour = false;
            }
        }
    }
}
