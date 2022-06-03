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
    private ObstacleBehaviour _obstacleBehaviour;
    private Transform boxStartPos;
    private SpriteRenderer boxSprite;
    
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
                
                if (pushToLeft == false)
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
                boxSprite = col.GetComponent<SpriteRenderer>();
                boxRB.velocity = Vector2.zero;
                _obstacleBehaviour = col.GetComponent<ObstacleBehaviour>();
                StartCoroutine(GoDown(boxPos,boxRB, transform));
                switchCol.enabled = false;
            }
        }
    }

    IEnumerator GoDown(Transform box, Rigidbody2D rb, Transform grabber)
    {
        boxStartPos = box;
        yield return new WaitForSeconds(0.5f);
        boxSprite.enabled = false;
        _obstacleBehaviour.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        boxOnPlace = true;
    }
    
    IEnumerator GoUp(Transform box, Rigidbody2D rb, Transform grabber)
    {
        box.position = boxStartPos.position;
        boxSprite.enabled = true;
        boxCol.isTrigger = false;
        _obstacleBehaviour.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxOnPlace = false;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up);
        Gizmos.DrawRay(transform.position, -transform.up);
        
    }
}
