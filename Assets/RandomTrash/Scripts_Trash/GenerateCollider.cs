using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCollider : MonoBehaviour
{
    private EdgeCollider2D coll;
    private LineRenderer lineRenderer;
    private List<Vector2> points;
    private Color groundColor;
    private Material groundMaterial;
    public Material GroundMaterial { set { groundMaterial = value; } }
    public Color GroundColor { set { groundColor = value; } }
    private int maxPoints;
    public int MaxPoints { set { maxPoints = value; } }
    [Range(0, 100), Tooltip("How much of the circle should the collider draw ?")]
    public int percentageCircle;
    public int PercentageCircle { set { percentageCircle = value; } }
    private float epaisseurSol;
    public float EpaisseurSol { set { epaisseurSol = value; } }

    [ContextMenu("Generate Ground")]
    public void GenerateGroundCollision()
    {
        if (gameObject.TryGetComponent<EdgeCollider2D>(out coll))
        {
            DestroyImmediate(coll);
        }

        points = new List<Vector2>();
        int pointNb = Mathf.RoundToInt((float)maxPoints * ((float)percentageCircle / 100f));
        for(int i = 0; i < pointNb + 1; i++)
        {
            Vector2 point = Vector2.right * 10f;
            point = Quaternion.Euler(0, 0, 360f * ((float)i / (float)maxPoints)) * point;
            points.Add(point);
        }

        coll = gameObject.AddComponent<EdgeCollider2D>();
        coll.SetPoints(points);

        GenerateGround();
    }

    private void GenerateGround()
    {
        float scale = transform.parent.parent.localScale.x;

        if (gameObject.TryGetComponent<LineRenderer>(out lineRenderer))
        {
            DestroyImmediate(lineRenderer);
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = points.Count;
        lineRenderer.startWidth = epaisseurSol * scale;
        lineRenderer.endWidth = epaisseurSol * scale;
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 a = points[i];
            a = a - a.normalized * (epaisseurSol / 2f);
            lineRenderer.SetPosition(i, new Vector3(a.x, a.y, -1.1f));
        }
        lineRenderer.sharedMaterial = groundMaterial;
        lineRenderer.startColor = groundColor;
        lineRenderer.endColor = groundColor;
        lineRenderer.numCapVertices = 4;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
    }

    public void UpdateGroundWidth()
    {
        float scale = transform.parent.parent.localScale.x;
        lineRenderer.startWidth = epaisseurSol * scale;
        lineRenderer.endWidth = epaisseurSol * scale;
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
