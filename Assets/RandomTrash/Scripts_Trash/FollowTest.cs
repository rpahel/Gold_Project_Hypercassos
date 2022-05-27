using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour
{
    public GameObject follow;
    public int distance = 20;
    public float speed = 0.1f;
    public List<Vector3> positionList;
    void FixedUpdate()
    {
        positionList.Add(transform.position);
        if(positionList.Count > distance)
        {
            positionList.RemoveAt(0);
            follow.transform.position = positionList[0];
        }
    }
}

