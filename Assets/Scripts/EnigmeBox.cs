using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnigmeBox : MonoBehaviour
{
    
    private BoxCollider2D switchCol;
    private Transform playerPos;
    private bool boxOnPlace;
    private GameObject box;
    private bool isActive = false;
    private RaycastHit2D hit;
    
    
    public bool pushToLeft;
   

   
    

    // Update is called once per frame
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        
        Vector3 right = transform.right.normalized;
        Vector3 direction  = (playerPos.position - transform.position).normalized;
        float dot = Vector3.Dot(right, direction);


        if (boxOnPlace && !isActive)
        {
            
            
            if (dot < -0.3)
            {
                
                if (pushToLeft == false)
                {
                    StartCoroutine(GoUp(box)); 
                    
                }
                
            }
            else if (dot > 0.3)
            {

                
                if (pushToLeft)
                {
                    StartCoroutine(GoUp(box));
                   
                    
                }
                
            }
            
        }
        if (isActive && !boxOnPlace)
        {


            if (dot < 0)
            {


                if (pushToLeft)
                {
                    StartCoroutine(GoDown(box));

                }

            }
            else if (dot > 0)
            {


                if (!pushToLeft)
                {
                    StartCoroutine(GoDown(box));


                }

            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            box = col.gameObject;
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            isActive = false;
        }
    }

    IEnumerator GoDown(GameObject box)
    {
        box.GetComponent<Animator>().SetBool("Down", true);
        boxOnPlace = true;
        isActive = false;
        yield return null;
    }
    
    IEnumerator GoUp(GameObject box)
    {
        box.GetComponent<Animator>().SetBool("Down", false);
        boxOnPlace = false;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up);
    }

    
}
