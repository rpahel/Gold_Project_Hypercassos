using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    public GameObject follow;
    public int distance = 20;
    public float speed = 0.1f;
    public List<Vector3> positionList;
    private GameObject player;
    private SpriteRenderer sprite;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Start()
    {
        player = transform.parent.gameObject.transform.GetChild(0).gameObject;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }
    void FixedUpdate()
    {
            //fair que les boules garde la rotation de la tete de l'asticot
            transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<Player>().Angle);
            
            //applique la position enregistrer la plus ancienne et on la supprime de la liste
            positionList.Add(transform.position);
            if (positionList.Count > distance && follow != null)
            {
                positionList.RemoveAt(0);
                follow.transform.position = positionList[0];
            }

    }
}

