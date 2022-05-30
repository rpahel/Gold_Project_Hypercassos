using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHoleColliders : MonoBehaviour
{
    public int nombreDeTrous;

    [ContextMenu("Generate Colliders")]
    public void GenerateCollider()
    {
        if (transform.childCount > 0)
        {
            foreach(Transform child in gameObject.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        if(nombreDeTrous == 0)
        {
            GameObject collider = new GameObject("Collider");
            collider.transform.SetParent(transform, false);
            GenerateCollider collScript = collider.AddComponent<GenerateCollider>();
            collScript.GenerateGround(100);
        }
        else
        {
            for(int i = 1; i < nombreDeTrous + 1; i++)
            {
                GameObject collider = new GameObject($"Collider {i}");
                collider.transform.SetParent(transform, false);
                GenerateCollider collScript = collider.AddComponent<GenerateCollider>();
                collScript.GenerateGround(100/nombreDeTrous - 3);
            }
        }

    }
}
