using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnigmeBox : MonoBehaviour
{
    public float endLine;
    private BoxCollider2D switchCol;
    private Transform playerPos;
    private bool boxOnPlace;
    public bool pushToLeft;

    private Transform boxPos;
    private Rigidbody2D boxRB;
    private BoxCollider2D boxCol;
    

    // Update is called once per frame
    void Start()
    {
        switchCol = GetComponent<BoxCollider2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        
        if (boxOnPlace)
        {
            if (playerPos.position.x < transform.position.x - 1)
            {
                if (!pushToLeft)
                {
                    StartCoroutine(GoUp(boxPos,boxRB, transform)); 
                }
                
            }
            else if (playerPos.position.x > transform.position.x + 1)
            {
                if (pushToLeft)
                {
                    StartCoroutine(GoUp(boxPos,boxRB, transform));  
                }
                
            }
            
        }
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            if (Math.Abs(switchCol.bounds.center.x - col.bounds.center.x) <= 0.1)
            {
                boxPos = col.GetComponent<Transform>();
                boxCol = col.GetComponent<BoxCollider2D>();
                boxCol.isTrigger = true;
                boxRB = col.GetComponent<Rigidbody2D>();
                boxRB.velocity = Vector2.zero;
                StartCoroutine(GoDown(boxPos,boxRB, transform));
                switchCol.enabled = false;
            }
        }
    }

    IEnumerator GoDown(Transform box, Rigidbody2D rb, Transform pressureSwitch)
    {
        box.position = pressureSwitch.position;
        yield return new WaitForSeconds(0.5f);
        box.position = Vector3.MoveTowards(box.position, 
            new Vector3(pressureSwitch.position.x, pressureSwitch.position.y - 0.7f, 0),1);
        rb.bodyType = RigidbodyType2D.Static;
        boxOnPlace = true;
    }
    
    IEnumerator GoUp(Transform box, Rigidbody2D rb, Transform pressureSwitch)
    {
        box.position = Vector3.MoveTowards(box.position,
            new Vector3(pressureSwitch.position.x, pressureSwitch.position.y + 1f, 0),1);
        boxCol.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxOnPlace = false;
        yield return null;
    }
   
}
