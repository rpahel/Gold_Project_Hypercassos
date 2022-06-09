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
        print("isactive" + isActive);
        print("box place" + boxOnPlace);

       // hit = Physics2D.Raycast(transform.position, transform.up);

        //if (hit.collider == null)
         //   isActive = false;
        
        //if (hit.collider !=null)
       // {
         //   if (hit.collider.tag == "Box" && isActive)
           // {
                
             //   box = hit.collider.gameObject;
               // StartCoroutine(GoDown(box));
               // isActive = true;
          //  }
        //}
        
        
        
        Vector3 right = transform.right.normalized;
        Vector3 direction  = (playerPos.position - transform.position).normalized;
        float dot = Vector3.Dot(right, direction);
        if (dot < 0)
        {
            print("player gauche");
        }
        else
            print("player droite");


        if (boxOnPlace && !isActive)
        {
            
            
            if (dot < -0.7)
            {
                
                if (pushToLeft == false)
                {
                    print("boite down mais perso bon coté gauche");
                    StartCoroutine(GoUp(box)); 
                    
                }
                
            }
            else if (dot > 0.7)
            {

                
                if (pushToLeft)
                {
                    print("boite down mais perso bon coté droit");
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
                    print("boite up mais perso mauvais coté gauche");
                    StartCoroutine(GoDown(box));

                }

            }
            else if (dot > 0)
            {


                if (!pushToLeft)
                {
                    print("boite up mais perso mauvais coté droit");
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
        print("DOWN");
        box.GetComponent<Animator>().SetBool("Down", true);
        boxOnPlace = true;
        isActive = false;
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
