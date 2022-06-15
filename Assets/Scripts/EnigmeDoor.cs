using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnigmeDoor : MonoBehaviour
{
    
    public bool isOpen;
    
    public AudioClip sfx;
    private SpriteRenderer sprite;
    private BoxCollider2D col;
    public Animator animator;
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
                animator.SetBool("Open", true);
                SfxManager.sfxInstance.audio.PlayOneShot(sfx);
                col.enabled = false;
            }
            else
            {
                animator.SetBool("Open", false);
                col.enabled = true;
            }
            
    }
    
    
}
