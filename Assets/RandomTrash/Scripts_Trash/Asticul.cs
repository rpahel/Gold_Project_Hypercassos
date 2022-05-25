using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asticul : MonoBehaviour
{
    public Vector2 worldCenter;
    public Vector2 toWorldCenter;
    public float gForce;

    public float speed;
    public float jumpForce;

    private float angle;
    private Rigidbody2D rb;
    private Vector2 gravityForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        toWorldCenter = worldCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.DrawRay(rb.position, transform.right * .51f, Color.red);

        if (!OnGround())
        {
            gravityForce -= (Vector2)transform.up * gForce * Time.deltaTime;
        }
        else
        {
            gravityForce = Vector2.zero;
        }

        rb.velocity = (Vector2)((transform.right * speed) * Input.GetAxis("Horizontal") - transform.up * 0.075f * Mathf.Abs(Input.GetAxis("Horizontal"))) + gravityForce;

    }

    private bool OnGround()
    {
        return Physics2D.Raycast(rb.position, -transform.up, 0.51f);
    }
}
