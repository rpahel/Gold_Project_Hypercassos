using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asticul : MonoBehaviour
{
    public Vector2 worldCenter;
    public float gForce;

    public float acceleration;
    public float stopForce;
    public float maxSpeed;
    public float jumpForce;

    private float angle;
    private Rigidbody2D rb;
    private Vector2 toWorldCenter;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        toWorldCenter = worldCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.DrawRay(rb.position, transform.right * .55f, Color.red);
        //Debug.DrawRay(rb.position, rb.velocity, Color.green);

        if (!OnGround())
        {
            rb.AddForce(-transform.up * gForce);
            isJumping = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            if(rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
                rb.AddForce(transform.right * acceleration);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
                rb.AddForce(-transform.right * acceleration);
        }
        else
        {
            float leftOrRight = Vector2.Dot(rb.velocity, transform.right);
            if(leftOrRight <= -0.1f)
            {
                rb.AddForce(transform.right * stopForce);
            }
            else if (leftOrRight >= 0.1f)
            {
                rb.AddForce(-transform.right * stopForce);
            }
            else
            {
                if(!isJumping)
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            // Les deux manières si dessous fonctionnent
            //rb.velocity += (Vector2)transform.up * jumpForce;
            rb.AddForce((Vector2)transform.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private bool OnGround()
    {
        return Physics2D.Raycast(rb.position, -transform.up, 0.55f);
    }
}
