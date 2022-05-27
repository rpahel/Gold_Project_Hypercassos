using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Header("Physics stuff")]
    public Vector2 worldCenter;
    public float gForce;
    private Vector2 oldWorlCenter;
    private float angle;
    private Rigidbody2D rb;
    private Vector2 toWorldCenter;
    private  Vector2 gravityForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        gravity();
    }
    private void gravity()
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
        rb.velocity = gravityForce;
    }

    private bool OnGround()
    {
        return Physics2D.Raycast(rb.position, -transform.up, 0.51f);
    }
}
