using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircle : MonoBehaviour
{
    public int numberOfCircle;
    public List<GameObject> followCircle;
    public GameObject circleprefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfCircle; i++)
        {
            GameObject go = Instantiate(circleprefabs, transform.position, transform.rotation);
            go.name = i.ToString();
            followCircle.Add(go);
            go.transform.SetParent(transform.parent);
        }
        Debug.Log("oui"+ followCircle.Count);
        GetComponent<FollowTest>().follow = transform.parent.GetChild(1).gameObject;
        for (int y = 1; y < numberOfCircle; y++)
        {
            Debug.Log("bite");
            transform.parent.GetChild(y).gameObject.GetComponent<FollowTest>().follow = transform.parent.GetChild(y + 1).gameObject;
            //followCircle[y].GetComponent<FollowTest>().follow = followCircle[y + 1];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
