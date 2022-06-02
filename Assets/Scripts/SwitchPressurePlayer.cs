using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPressurePlayer : MonoBehaviour
{
    private SpriteRenderer sprite;
    [HideInInspector]public bool isActive;
    
    public List<EnigmeDoor> DoortoClose;
    public List<EnigmeDoor> DoortoOpen;

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
        
        for (int i = 0; i < DoortoOpen.Count; i++)
        {
            DoortoOpen[i].isOpen = isActive;
        }
        
        for (int i = 0; i < DoortoClose.Count; i++)
        {
            DoortoClose[i].isOpen = !isActive;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isActive = true;
        }
    }
}
