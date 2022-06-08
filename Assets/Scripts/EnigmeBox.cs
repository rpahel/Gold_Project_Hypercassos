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
    
    
    public bool pushToLeft;
   

   
    

    // Update is called once per frame
    void Start()
    {
        switchCol = GetComponent<BoxCollider2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 right = transform.right.normalized;
        Vector3 direction  = (playerPos.position - transform.position).normalized;
        float dot = Vector3.Dot(right, direction);
        
        
        if (boxOnPlace && !isActive)
        {
            
            
            if (dot < 0)
            {
                print("A gauche");
                
                if (pushToLeft == false)
                {
                    StartCoroutine(GoUp(box)); 
                    
                }
                
            }
            else if (dot > 0)
            {

                print("A droite");
                if (pushToLeft)
                {
                    StartCoroutine(GoUp(box));
                   
                    
                }
                
            }
            
        }
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Box" && isActive)
        {
            if (Math.Abs(switchCol.bounds.center.x - col.bounds.center.x) <= 0.1)
            {
                box = col.gameObject;
                StartCoroutine(GoDown(box));
                isActive = false;

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

    
}
