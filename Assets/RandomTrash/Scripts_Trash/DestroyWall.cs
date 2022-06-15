using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="ExplosiveBox")
        {
            collision.gameObject.GetComponent<ObstacleBehaviour>().destroyBox();
            GetComponent<Animator>().SetBool("Explode", true);
        }
    }
}
