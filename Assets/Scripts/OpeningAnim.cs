using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAnim : MonoBehaviour
{
    
    public Animator animator;
    public bool AutoLaunch;

    void Start()
    {
        if (AutoLaunch)
        {
            animator.SetTrigger("Open");
        }
    }
}
