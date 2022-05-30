using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaisPasVraiment : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 30f;
    private Vector2 mov;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        mov.x = Input.GetAxis("Horizontal");
        

        rb.velocity = new Vector2(mov.x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

}
