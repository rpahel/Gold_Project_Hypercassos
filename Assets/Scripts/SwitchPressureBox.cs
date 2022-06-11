using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPressureBox : MonoBehaviour
{
    private SpriteRenderer sprite;
    public bool isActive;
    
    public List<EnigmeDoor> DoortoClose;
    public List<EnigmeDoor> DoortoOpen;
    public List<elevator> Elevators;
    public Animator animator;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        for (int i = 0; i < DoortoOpen.Count; i++)
        {
            DoortoOpen[i].isOpen = isActive;
        }
        
        for (int i = 0; i < DoortoClose.Count; i++)
        {
            DoortoClose[i].isOpen = !isActive;
        }
        
        for (int i = 0; i < Elevators.Count; i++)
        {
            Elevators[i].isMoving = isActive;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Box" || col.tag == "Player")
        {
            isActive = true;
            animator.SetBool("Down", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Box" || col.tag == "Player")
        {
            isActive = false;
            animator.SetBool("Down", false);
        }
    }
    
    
}
