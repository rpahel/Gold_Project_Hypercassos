using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownLayer : MonoBehaviour
{
    private LevelBehaviour level;
    public GameObject oposite;
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
            StartCoroutine(coliTimer());
        }
    }
    IEnumerator coliTimer()
    {
        oposite.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        oposite.GetComponent<BoxCollider2D>().enabled = true;
    }

}
