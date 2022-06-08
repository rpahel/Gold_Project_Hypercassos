using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ASTITOUCH : MonoBehaviour
{
    private SpriteRenderer sprite;

    [Header("Physics stuff")]
    private Vector2 worldCenter;
    public float gForce;
    //private Vector2 oldWorlCenter;
    [HideInInspector]public float angle;
    private Rigidbody2D rb;
    private Vector2 toWorldCenter;
    private Vector2 gravityForce;
    private CircleCollider2D coll;
    public bool isClone;
    [Header("Movement stuff")]
    public float speed;
    public float jumpForce;
    public float acceleration;
    public float freinage;
    private float sens;
    public GameObject vcam;
    private bool frozen;

    [Header("Zone légale")]
    public float limitStart;
    public float limitEnd;
    public float tpPoint;
    private Vector2 limitStartPos;
    private Vector2 limitEndPos;
    private Vector2 tpPointPos;

    //private Vector3 cameraLocalPosition;
    private void Awake()
    {
        worldCenter = new Vector2(0, -11);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
        //oldWorlCenter = worldCenter;
        //followCamera = gameObject.transform.GetChild(0).gameObject;
        //cameraLocalPosition = followCamera.transform.position;
    }

    private void Start()
    {
        if(!isClone)
        {
            frozen = true;
        }
    }

    private void Update()
    {
        if (frozen)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (!isClone)
        {
            DrawLimits();
            CheckLimits();
        }

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

        if(Input.touchCount > 0) // Touch controls
        {
            float xPos = Input.GetTouch(0).position.x;
            if (xPos > (3 * (Screen.width / 5f)))
            {
                sens += Time.deltaTime * acceleration;
                sens = Mathf.Clamp(sens, 0f, 1f);
            }
            else if (xPos < (2 * (Screen.width / 5f)))
            {
                sens -= Time.deltaTime * acceleration;
                sens = Mathf.Clamp(sens, -1f, 0f);
            }
            else
            {
                if (sens > 0f)
                {
                    sens -= Time.deltaTime * freinage;
                    sens = Mathf.Clamp(sens, 0f, 1f);
                }
                else if (sens < 0f)
                {
                    sens += Time.deltaTime * freinage;
                    sens = Mathf.Clamp(sens, -1f, 0f);
                }
            }
        }
        else if (Input.GetAxis("Horizontal") != 0f) // Keyboard controls
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                sens += Time.deltaTime * acceleration;
                sens = Mathf.Clamp(sens, 0f, 1f);
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                sens -= Time.deltaTime * acceleration;
                sens = Mathf.Clamp(sens, -1f, 0f);
            }
        }
        else
        {
            if(sens > 0f)
            {
                sens -= Time.deltaTime * freinage;
                sens = Mathf.Clamp(sens, 0f, 1f);
            }
            else if(sens < 0f)
            {
                sens += Time.deltaTime * freinage;
                sens = Mathf.Clamp(sens, -1f, 0f);
            }
        }

        Vector2 move = (transform.right * speed) * sens;
        Vector2 correction = transform.up * 0.075f * Mathf.Abs(sens);
        rb.velocity = (move - correction) + gravityForce;

        if(sens < 0)
        {
            sprite.flipX = false;
        }
        else if (sens > 0)
        {
            sprite.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Climbable")
        {

            //oldWorlCenter = worldCenter;
            worldCenter = collision.transform.position;
            //followCamera.transform.SetParent(null);
            
        }
        else if (collision.gameObject.tag == "Box")
        {
            if(collision.gameObject.GetComponent<ObstacleBehaviour>().canClimb)
            {
                //oldWorlCenter = worldCenter;
                worldCenter = collision.transform.position;
            }

        }
        else if (collision.gameObject.tag == "BoxBlocker")
        {
            //oldWorlCenter = worldCenter;
            worldCenter = collision.transform.position;
        }
        else if(collision.gameObject.tag == "ExplosiveBox")
        {
            if (collision.gameObject.GetComponent<ObstacleBehaviour>().canClimb)
            {
                //oldWorlCenter = worldCenter;
                worldCenter = collision.transform.position;
            }
        }
        else if(collision.gameObject.tag =="Earth")
        {

            worldCenter = new Vector2(0, -11);
            //followCamera.transform.SetParent(gameObject.transform);
            //followCamera.transform.position = cameraLocalPosition;
            //followCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private bool OnGround()
    {
        Debug.DrawRay(rb.position, -transform.up * 0.52f, Color.blue);
        return Physics2D.Raycast(rb.position, -transform.up, 0.52f);
    }

    /// <summary>
    /// Freezes the player's controls and physics.
    /// </summary>
    public void Freeze()
    {
        frozen = true;
        coll.enabled = false;
    }

    /// <summary>
    /// Unfreezes the player so that he can move and be affected by gravity.
    /// </summary>
    public void UnFreeze()
    {
        frozen = false;
        coll.enabled = true;
    }

    private void DrawLimits()
    {
        CalculateLimits();
        Debug.DrawLine(limitStartPos, limitEndPos, Color.red);
    }

    private void CalculateLimits()
    {
        Vector2 playerPos = (Vector2)transform.position - new Vector2(0, -11f);
        limitStartPos = new Vector2(0, -11f) + playerPos.normalized * limitStart;
        limitEndPos = new Vector2(0, -11f) + playerPos.normalized * limitEnd;
        tpPointPos = new Vector2(0, -11f) + playerPos.normalized * tpPoint;
    }

    private void CheckLimits()
    {
        Vector2 playerPos = (Vector2)transform.position - new Vector2(0, -11f);
        if (playerPos.sqrMagnitude >= limitEnd * limitEnd || playerPos.sqrMagnitude <= limitStart * limitStart)
        {
            transform.position = tpPointPos;
        }
    }
}