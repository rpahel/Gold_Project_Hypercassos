using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbablebox : MonoBehaviour
{
    public bool climbable;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coli "+collision.gameObject.tag);
    }
}
