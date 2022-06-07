using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    private SpriteRenderer sprite;

    [Header("Physics stuff")]
    private Vector2 gravityCenter;
    private Vector2 worldCenter;
    private Vector2 toWorldCenter;
    private Vector2 gravityForce;
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    [SerializeField] private float gForce;
    private float angle;

    [Header("Movement stuff")]
    public bool isClone;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float freinage;
    private float sens;
    private bool frozen;

    [Header("Legal zone")]
    [SerializeField] private float limitStart;
    [SerializeField] private float limitEnd;
    [SerializeField] private float tpPoint;
    private Vector2 limitStartPos;
    private Vector2 limitEndPos;
    private Vector2 tpPointPos;

    // Properties
    public float Angle { get { return angle; } }
    public float Speed { get { return speed; } set { speed = value; } }

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Awake()
    {
        worldCenter = new Vector2(0, -11);
        gravityCenter = worldCenter;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        if (!isClone)
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
        
        CheckLimits();
        DrawLimits();
        CalculatePhysicsValues();
        GetInput();
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Climbable" || tag == "BoxBlocker")
        {
            gravityCenter = collision.transform.position;
        }
        else if (tag == "Box" || tag == "ExplosiveBox")
        {
            if(collision.gameObject.GetComponent<ObstacleBehaviour>().canClimb)
            {
                gravityCenter = collision.transform.position;
            }
        }
        else if(tag == "Earth")
        {
            gravityCenter = worldCenter;
        }
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    private bool OnGround()
    {
        Debug.DrawRay(rb.position, -transform.up * 0.52f, Color.blue);
        return Physics2D.Raycast(rb.position, -transform.up, 0.52f);
    }

    private void CalculatePhysicsValues()
    {
        toWorldCenter = gravityCenter - rb.position;
        angle = Vector2.SignedAngle(Vector2.up, -toWorldCenter);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (!OnGround())
        {
            gravityForce -= gForce * Time.deltaTime * (Vector2)transform.up;
        }
        else
        {
            gravityForce = Vector2.zero;
        }
    }

    private void Movement()
    {
        Vector2 move = (transform.right * speed) * sens;
        Vector2 correction = transform.up * 0.075f * Mathf.Abs(sens);
        rb.velocity = (move - correction) + gravityForce;

        if (sens < 0)
        {
            sprite.flipX = false;
        }
        else if (sens > 0)
        {
            sprite.flipX = true;
        }
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

    private void CalculateLimits()
    {
        Vector2 playerPos = (Vector2)transform.position - gravityCenter;
        limitStartPos = gravityCenter + playerPos.normalized * limitStart;
        limitEndPos = gravityCenter + playerPos.normalized * limitEnd;
        tpPointPos = gravityCenter + playerPos.normalized * tpPoint;
    }

    /// <summary>
    /// Checks if the player is within the legal vertical range. If not, teleports to choosen safe point.
    /// </summary>
    private void CheckLimits()
    {
        CalculateLimits();
        Vector2 playerPos = (Vector2)transform.position - gravityCenter;
        if (playerPos.sqrMagnitude >= limitEnd * limitEnd || playerPos.sqrMagnitude <= limitStart * limitStart)
        {
            transform.position = tpPointPos;
        }
    }

    /// <summary>
    /// Draws a line representing the limits of the playable vertical range of the player.
    /// </summary>
    private void DrawLimits()
    {
        Debug.DrawLine(limitStartPos, limitEndPos, Color.red);
    }

    /// <summary>
    /// Get either the touch or the keyboard input.
    /// </summary>
    private void GetInput()
    {
        if (Input.touchCount > 0) // Touch controls
        {
            float xPos = Input.GetTouch(0).position.x;
            if (xPos > (3 * (Screen.width / 5f)))
            {
                GoRight();
            }
            else if (xPos < (2 * (Screen.width / 5f)))
            {
                GoLeft();
            }
            else
            {
                Brake();
            }
        }
        else if (Input.GetAxis("Horizontal") != 0f) // Keyboard controls
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                GoRight();
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                GoLeft();
            }
        }
        else
        {
            Brake();
        }
    }

    private void GoRight()
    {
        sens += Time.deltaTime * acceleration;
        sens = Mathf.Clamp(sens, 0f, 1f);
    }

    private void GoLeft()
    {
        sens -= Time.deltaTime * acceleration;
        sens = Mathf.Clamp(sens, -1f, 0f);
    }

    /// <summary>
    /// Slow down until stopping.
    /// </summary>
    private void Brake()
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