using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    public Vector2          worldCenter;
    public  bool            enableGravity;
    public  bool            dontFreezeOnSwitchLayer;

    private float           gForce;
    private float           angle;
    private Rigidbody2D     rb;
    private Vector2         toWorldCenter;
    private Vector2         gravityForce;
    private BoxCollider2D   coll;
    [HideInInspector]
    public  bool            canClimb;
    [HideInInspector]
    public  bool            isClone;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        gForce = 10;
    }

    void Update()
    {
        Gravity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BoxBlocker" && OnGround())
        {
            canClimb = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "BoxBlocker")
    //    {
    //        canClimb = false;
    //    }
    //}

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    public void DestroyBox()
    {
        Destroy(gameObject);
    }

    private void Gravity()
    {
        toWorldCenter = worldCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);

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
        RaycastHit2D[] hits = new RaycastHit2D[3];
        Vector2 iniOrigin = transform.position + Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, transform.right)) * new Vector2(coll.offset.x * transform.lossyScale.x, coll.offset.y * transform.lossyScale.y);

        for (int i = 0; i < hits.Length; i++)
        {
            Vector2 origin = iniOrigin;
            Vector2 direction;

            if (i < 2)
            {
                origin -= (Vector2)transform.right * (coll.size.x * 0.5f + coll.edgeRadius) * transform.lossyScale.x;
                origin += i * ((Vector2)transform.right * (coll.size.x + coll.edgeRadius * 2f) * transform.lossyScale.x);
                origin -= (Vector2)transform.up * (coll.size.y * 0.5f + coll.edgeRadius + 0.02f) * transform.lossyScale.y;
                direction = ((iniOrigin - (Vector2)transform.up * (coll.size.y * 0.5f + coll.edgeRadius + 0.02f) * transform.lossyScale.y) - origin).normalized * (coll.size.x + coll.edgeRadius * 2f) * transform.lossyScale.x;
            }
            else
            {
                direction = -transform.up * (coll.size.y * 0.5f + coll.edgeRadius + 0.02f) * transform.lossyScale.y;
            }

            hits[i] = Physics2D.Raycast(origin, direction.normalized, direction.magnitude);
            if (hits[i])
            {
                Debug.DrawRay(origin, direction, Color.blue);
                return true;
            }
        }
        return false;
    }

    public void MakeOrphan()
    {
        transform.parent = null;
    }
}
