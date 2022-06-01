using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnigmeDoor : MonoBehaviour
{
    public SwitchPressureBox boxSwitch;
    public SwitchPressurePlayer playerSwitch;
    
    [Tooltip("True = door open with player switch, False = door open with box switch")]
    public bool isPlayerSwitch;

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
        if (isPlayerSwitch)
        {
            if (playerSwitch.isActive)
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
        else
        {
            if (boxSwitch.isActive)
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
    
    
}
