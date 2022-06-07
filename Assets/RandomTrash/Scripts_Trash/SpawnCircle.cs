using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircle : MonoBehaviour
{
    public int numberOfCircle;
    public List<GameObject> followCircle;
    public GameObject circleprefabs;
    private int orderInLayerSprite; // pour que le sprite des object qui spawn soit dans le bon ordre

    void Start()
    {
        orderInLayerSprite = GetComponent<SpriteRenderer>().sortingOrder;
        for (int i = 0; i < numberOfCircle; i++)//pour le nombre de boule demander
        {
            orderInLayerSprite--;
            GameObject go = Instantiate(circleprefabs, transform.position, transform.rotation);
            go.name = i.ToString();
            followCircle.Add(go);
            go.transform.SetParent(transform.parent);
            go.GetComponent<SpriteRenderer>().sortingOrder = orderInLayerSprite;

            if(i == numberOfCircle-1) // la taille de la derniere boule est plus petite
            {
                go.transform.localScale = new Vector3(0.701626897f, 0.701626897f, 0.701626897f);
            }

           
        }
        GetComponent<FollowTest>().follow = transform.parent.GetChild(1).gameObject;
        for (int y = 1; y < numberOfCircle; y++)//fait que la boule suis celle d'avant
        {
           
            transform.parent.GetChild(y).gameObject.GetComponent<FollowTest>().follow = transform.parent.GetChild(y + 1).gameObject;
            

        }
    }

}
