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
    private bool isActive = true;
    private RaycastHit2D hit;
    
    
    public bool pushToLeft;
   

   
    

    // Update is called once per frame
    void Start()
    {
        switchCol = GetComponent<BoxCollider2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.up);
        
        if(hit.collider ==null)
            return;
        if (hit !=null)
        {
            if (hit.collider.tag == "Box" && isActive)
            {
                
                box = hit.collider.gameObject;
                StartCoroutine(GoDown(box));
                isActive = false;
            }
        }
        
        
        
        Vector3 right = transform.right.normalized;
        Vector3 direction  = (playerPos.position - transform.position).normalized;
        float dot = Vector3.Dot(right, direction);
        
        
        if (boxOnPlace && !isActive)
        {
            
            
            if (dot < 0)
            {
               
                
                if (pushToLeft == false)
                {
                    StartCoroutine(GoUp(box)); 
                    
                }
                
            }
            else if (dot > 0)
            {

                
                if (pushToLeft)
                {
                    StartCoroutine(GoUp(box));
                   
                    
                }
                
            }
            
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            isActive = true;
        }
    }

    IEnumerator GoDown(GameObject box)
    {
        print("DOWN");
        box.GetComponent<Animator>().SetBool("Down", true);
        boxOnPlace = true;
        yield return null;
    }
    
    IEnumerator GoUp(GameObject box)
    {
        print("UP");
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
