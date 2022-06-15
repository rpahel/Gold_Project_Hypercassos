using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public AudioClip sfx;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="ExplosiveBox")
        {
            collision.gameObject.GetComponent<ObstacleBehaviour>().DestroyBox();
            SfxManager.sfxInstance.audio.PlayOneShot(sfx);
            GetComponent<Animator>().SetBool("Explode", true);
        }
    }
}
