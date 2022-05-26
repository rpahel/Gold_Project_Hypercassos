using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Physics stuff")]
    public Vector2 worldCenter;
    public float gForce;

    private float angle;
    private Rigidbody2D rb;
    private Vector2 toWorldCenter;
    private Vector2 gravityForce;

    [Header("Movement stuff")]
    public float speed;
    //public float jumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        toWorldCenter = worldCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //Debug.DrawRay(rb.position, transform.right * .51f, Color.red);

        if (!OnGround())
        {
            gravityForce -= (Vector2)transform.up * gForce * Time.deltaTime;
        }
        else
        {
            gravityForce = Vector2.zero;
        }

        //if(Input.GetKey(KeyCode.Space))
        //{
        //    rb.AddForce(transform.up* jumpForce);
        //}
        Vector2 move = (transform.right * speed) * Input.GetAxis("Horizontal");
        Vector2 correction = transform.up * 0.075f * Mathf.Abs(Input.GetAxis("Horizontal"));
        rb.velocity = (move - correction) + gravityForce;
    }

    private bool OnGround()
    {
        return Physics2D.Raycast(rb.position, -transform.up, 0.51f);
    }
}