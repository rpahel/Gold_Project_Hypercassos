using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPressureBox : MonoBehaviour
{
    private SpriteRenderer sprite;
    [HideInInspector]public bool isActive;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isActive)
            sprite.color = Color.green;
        else
            sprite.color = Color.yellow;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Box" || col.tag == "Player")
        {
            isActive = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Box" || col.tag == "Player")
        {
            isActive = false;
        }
    }
}
