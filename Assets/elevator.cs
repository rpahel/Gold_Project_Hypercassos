using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField]private Transform pos1;                                 
    [SerializeField]private Transform pos2;
    
    [Tooltip("Active the elevator")]
    public bool isMoving = false;
    
    private bool retour;                                            
    
    void Update()
    {
        if (isMoving)
        {
            StartCoroutine(Move());
        }
        

    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        Transform childCol = col.collider.GetComponent<Transform>();
        childCol.localScale = new Vector3(1, 1, 1);
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
