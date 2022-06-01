using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPressurePlayer : MonoBehaviour
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
            sprite.color = Color.red;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isActive = true;
        }
    }
}
