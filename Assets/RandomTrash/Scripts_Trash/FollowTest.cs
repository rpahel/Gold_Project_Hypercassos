using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour
{
    public GameObject follow;
    public int distance = 20;
    public float speed = 0.1f;
    public List<Vector3> positionList;//liste qui stocke la position de la boule précédente a chaque frame #shlague
    private GameObject player;//tete de l'asticot
    private SpriteRenderer sprite;//sprite de boule
    private void Start()
    {
        player = transform.parent.gameObject.transform.GetChild(0).gameObject;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine(ActiveComponent());
    }
    void FixedUpdate()
    {
            //fair que les boules garde la rotation de la tete de l'asticot
            transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<ASTITOUCH>().angle);
            
            //applique la position enregistrer la plus ancienne et on la supprime de la liste
            positionList.Add(transform.position);
            if (positionList.Count > distance && follow != null)
            {
                positionList.RemoveAt(0);
                follow.transform.position = positionList[0];
            }

    }
    IEnumerator ActiveComponent()//pour faire un effet d'apparaition mais avec la gestion du jeu actuel ça marche pas
    {
        
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }
}

