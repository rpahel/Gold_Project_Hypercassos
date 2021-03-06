using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPressureBox : MonoBehaviour
{
    public AudioClip sfx;
    
    private SpriteRenderer sprite;
    public bool isActive;
    
    public List<EnigmeDoor> DoortoClose;
    public List<EnigmeDoor> DoortoOpen;
    public List<elevator> Elevators;
    public Animator animator;
    public bool boxIn = false;

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
        if (col.tag == "Box")
        {
            isActive = true;
            SfxManager.sfxInstance.audio.PlayOneShot(sfx);
            animator.SetBool("Down", true);
            boxIn = true;
        }
        else if (col.tag == "Player" && !boxIn)
        {
            isActive = true;
            animator.SetBool("Down", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            isActive = false;
            animator.SetBool("Down", false);
            boxIn = false;
        }
        else if (col.tag == "Player" && !boxIn)
        {
            isActive = false;
            animator.SetBool("Down", false);
        }
    }
    
    
}
