using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCollider : MonoBehaviour
{
    private EdgeCollider2D coll;
    private List<Vector2> points;
    public int maxPoints;
    [Range(0, 100), Tooltip("How much of the circle should the collider draw ?")]
    public int percentageCircle;

    [ContextMenu("Generate Collider")]
    public void GenerateGround()
    {
        if (gameObject.TryGetComponent<EdgeCollider2D>(out coll))
        {
            DestroyImmediate(coll);
        }

        points = new List<Vector2>();
        int pointNb = Mathf.RoundToInt((float)maxPoints * ((float)percentageCircle / 100f));
        for(int i = 0; i < pointNb + 1; i++)
        {
            Vector2 point = new Vector2(1, 1).normalized * 10f;
            point = Quaternion.Euler(0, 0, 360f * ((float)i / (float)maxPoints)) * point;
            points.Add(point);
        }

        coll = gameObject.AddComponent<EdgeCollider2D>();
        coll.SetPoints(points);
    }

    public void GenerateGround(int percentage)
    {
        if (gameObject.TryGetComponent<EdgeCollider2D>(out coll))
        {
            DestroyImmediate(coll);
        }

        points = new List<Vector2>();
        int pointNb = Mathf.RoundToInt((float)maxPoints * ((float)percentage / 100f));
        for (int i = 0; i < pointNb + 1; i++)
        {
            Vector2 point = new Vector2(1, 1).normalized * 10f;
            point = Quaternion.Euler(0, 0, 360f * ((float)i / (float)maxPoints)) * point;
            points.Add(point);
        }

        coll = gameObject.AddComponent<EdgeCollider2D>();
        coll.SetPoints(points);
    }
}
