using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StopWatch : MonoBehaviour
{
    public bool StopWatchActive;
    private float currentTime;
    public TimeSpan time;
    
    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if (StopWatchActive == true)
        {
            currentTime += Time.deltaTime;
            
        }

        time = TimeSpan.FromSeconds(currentTime);
        
    }

    
    
}
