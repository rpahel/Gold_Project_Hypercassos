using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownLayer : MonoBehaviour
{
    private LevelBehaviour level;
    private void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fall");
            level.RequestLayerDown();

        }
    }
}
