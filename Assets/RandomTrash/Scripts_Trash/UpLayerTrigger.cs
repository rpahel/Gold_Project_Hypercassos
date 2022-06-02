using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLayerTrigger : MonoBehaviour
{
    private LevelBehaviour level;
    public GameObject oposite;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fall");
            level.RequestLayerUp();
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
