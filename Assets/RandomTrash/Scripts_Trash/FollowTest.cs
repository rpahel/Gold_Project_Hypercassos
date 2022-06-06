using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour
{
    public GameObject follow;
    public int distance = 20;
    public float speed = 0.1f;
    public List<Vector3> positionList;
    private GameObject player;
    private SpriteRenderer sprite;
    private void Start()
    {
        player = transform.parent.gameObject.transform.GetChild(0).gameObject;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine(ActiveComponent());


    }
    void FixedUpdate()
    {

            transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<ASTITOUCH>().angle);
            positionList.Add(transform.position);
            if (positionList.Count > distance && follow != null)
            {
                positionList.RemoveAt(0);
                follow.transform.position = positionList[0];
            }

    }
    IEnumerator ActiveComponent()
    {
        
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }
}

