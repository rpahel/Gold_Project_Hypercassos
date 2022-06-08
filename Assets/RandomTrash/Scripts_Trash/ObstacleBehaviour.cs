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
    private SpriteRenderer sprite;
    public bool enableGravity; 
    public bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(waitToGravity());
    }

    // Update is called once per frame
    void Update()
    {
        if(enableGravity)
        {
            gravity();
        }
        
       
    }
    private void gravity()
    {
        toWorldCenter = worldCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //Debug.Log(rb.velocity);
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
        RaycastHit2D hit = Physics2D.Raycast(rb.position, -transform.up, 0.52f);
        //Debug.Log(hit);
        Debug.DrawRay(rb.position, -transform.up*(transform.lossyScale.x*0.52f),Color.blue);
        return Physics2D.Raycast(rb.position, -transform.up,(transform.lossyScale.x * 0.52f));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoxBlocker")
        {
            canClimb = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoxBlocker")
        {
            canClimb = false;
        }
    }
    public void destroyBox()
    {
        Destroy(gameObject);
    }
    IEnumerator waitToGravity()
    {
        yield return new WaitForSeconds(3f);
        enableGravity = true;
    }
}
