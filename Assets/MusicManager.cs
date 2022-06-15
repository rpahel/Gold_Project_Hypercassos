using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audio;
    public static MusicManager musicInstance;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (musicInstance != null && musicInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        musicInstance = this;
        DontDestroyOnLoad(this);
    }
    
}
