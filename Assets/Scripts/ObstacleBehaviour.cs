using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Header("Physics stuff")]
    public  Vector2         worldCenter;
    public  float           gForce;
    private Vector2         oldWorlCenter;
    private float           angle;
    private Rigidbody2D     rb;
    private Vector2         toWorldCenter;
    private Vector2         gravityForce;
    private SpriteRenderer  sprite;
    public  bool            enableGravity; 
    public  bool            canClimb;
    
    public bool             isClone;

    public Transform tLeft, tRight;
    
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
        //Debug.Log(rb.velocity);
        //Debug.DrawRay(rb.position, transform.right * .51f, Color.red);


        Debug.Log(OnGround());
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
        //Vector3 left = sprite.bounds.max-(transform.right*1.3f);
        //Vector3 right= sprite.bounds.max - (transform.right * 0.3f);
        Debug.DrawRay(tLeft.position, -transform.up * (transform.lossyScale.x * 0.15f), Color.blue);
        Debug.DrawRay(tRight.position, -transform.up * (transform.lossyScale.x * 0.15f), Color.blue);
        return (Physics2D.Raycast(transform.GetChild(0).transform.position, -transform.up, (transform.lossyScale.x * 0.15f)) ||
                Physics2D.Raycast(transform.GetChild(1).transform.position, -transform.up, (transform.lossyScale.x * 0.15f)));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BoxBlocker")
        {
            canClimb = true;
        }
        Debug.Log("colision "+collision.gameObject.tag);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoxBlocker")
        {
            canClimb = false;
            Debug.Log("collision exit");
        }
    }
    public void destroyBox()
    {
        GetComponent<Animator>().SetBool("Explode", true);
    }
    IEnumerator waitToGravity()
    {
        yield return new WaitForSeconds(3f);
        enableGravity = true;
    }
}
