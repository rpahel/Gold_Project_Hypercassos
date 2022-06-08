using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnigmeDoor : MonoBehaviour
{
    
    [HideInInspector] public bool isOpen;
    

    private SpriteRenderer sprite;
    private BoxCollider2D col;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (isOpen)
            {
                sprite.enabled = false;
                col.enabled = false;
            }
            else
            {
                sprite.enabled = true;
                col.enabled = true;
            }
            
    }
    
    
}
