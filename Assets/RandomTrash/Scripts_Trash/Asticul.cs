using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asticul : MonoBehaviour
{
    private SpriteRenderer sprite;

    [Header("Physics stuff")]
    public Vector2 worldCenter;
    public float gForce;
    private Vector2 oldWorlCenter;
    private float angle;
    private Rigidbody2D rb;
    private Vector2 toWorldCenter;
    private Vector2 gravityForce;

    [Header("Movement stuff")]
    public float speed;
    public float jumpForce;


    public GameObject vcam;
    //private Vector3 cameraLocalPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        oldWorlCenter = worldCenter;
        //followCamera = gameObject.transform.GetChild(0).gameObject;
        //cameraLocalPosition = followCamera.transform.position;
        
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
        if(Input.GetAxis("Horizontal")<=0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Climbable")
        {
            oldWorlCenter = worldCenter;
            worldCenter = collision.transform.position;
            //followCamera.transform.SetParent(null);
            
        }
        else if(collision.gameObject.tag =="Earth")
        {

            worldCenter = new Vector2(0, -10);
            //followCamera.transform.SetParent(gameObject.transform);
            //followCamera.transform.position = cameraLocalPosition;
            //followCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        Debug.Log(collision.gameObject.tag);
    }
    private bool OnGround()
    {
        return Physics2D.Raycast(rb.position, -transform.up, 0.51f);
    }
}