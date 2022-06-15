using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehaviour : MonoBehaviour
{
    private GameObject followingBodyCircle;
    public GameObject FollowingBodyCircle { set { followingBodyCircle = value; } }

    private List<Vector2> positionList;

    private int bodyCirclesCount;
    public int BodyCirclesCount { set { bodyCirclesCount = value; } }

    private void Awake()
    {
        positionList = new List<Vector2>();
    }

    private void FixedUpdate()
    {
        if (followingBodyCircle)
        {
            followingBodyCircle.transform.rotation = transform.rotation;
            positionList.Add(transform.position);
            if (positionList.Count > bodyCirclesCount * 0.5f) // On peut controler la taille du ver avec cette ligne. A tester
            {
                positionList.RemoveAt(0);
                followingBodyCircle.transform.position = positionList[0];
            }
        }
    }

    public void AddFollowingBodyCircle(GameObject _object)
    {
        if (!followingBodyCircle)
        {
            followingBodyCircle = _object;
        }
        else
        {
            followingBodyCircle.GetComponent<BodyBehaviour>().AddFollowingBodyCircle(_object);
        }
    }

    public void DestroyBodyCircle()
    {
        if (followingBodyCircle)
        {
            followingBodyCircle.GetComponent<BodyBehaviour>().DestroyBodyCircle();
        }

        Destroy(gameObject);
    }

    public void HideBodyCircle()
    {
        if (followingBodyCircle)
        {
            followingBodyCircle.GetComponent<BodyBehaviour>().HideBodyCircle();
        }

        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ShowBodyCircle()
    {
        if (followingBodyCircle)
        {
            followingBodyCircle.GetComponent<BodyBehaviour>().ShowBodyCircle();
        }

        GetComponent<SpriteRenderer>().enabled = true;
    }
}
