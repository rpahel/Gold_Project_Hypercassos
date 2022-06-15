using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideCheckTest : MonoBehaviour
{
    public CircleCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;

    void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (InsideCheck(playerCollider, coll))
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }

    public bool InsideCheck(CircleCollider2D playerCollider, BoxCollider2D boxCollider)
    {
        float circleRadius = playerCollider.radius;
        Vector2 circlePosition = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.center.y);
        Vector2 rectangleExtents = new Vector2(boxCollider.size.x * boxCollider.transform.lossyScale.x * 0.5f, boxCollider.size.y * boxCollider.transform.lossyScale.y * 0.5f);
        Vector2 rectanglePosition = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y);
        Vector2 half_height = boxCollider.transform.up * rectangleExtents.y;
        Vector2 half_width = boxCollider.transform.right * rectangleExtents.x;
        Vector2 boxToCircle = circlePosition - rectanglePosition;
        //Debug.DrawRay(rectanglePosition, boxToCircle);
        //Debug.DrawRay(rectanglePosition, half_height, Color.blue);
        //Debug.DrawRay(rectanglePosition, half_width, Color.yellow);

        if (Vector2.Dot(boxToCircle, boxCollider.transform.up) > half_height.magnitude + circleRadius || Vector2.Dot(boxToCircle, boxCollider.transform.right) > half_width.magnitude + circleRadius)
        {
            return false;
        }

        if (Vector2.Dot(boxToCircle, boxCollider.transform.up) < -half_height.magnitude - circleRadius || Vector2.Dot(boxToCircle, boxCollider.transform.right) < -half_width.magnitude - circleRadius)
        {
            return false;
        }

        return true;
    }
}
