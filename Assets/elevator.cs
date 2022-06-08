using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float speed = 2f;
    public float growspeed = 3f;
    [SerializeField]private Transform pos1;                                 
    [SerializeField]private Transform pos2;
    
    [Tooltip("Active the elevator")]
    public bool isMoving = false;
    public SpriteRenderer spriteRenderer;
    public Sprite off;
    public Sprite on;
    public Vector2 tailleHaute;
    public Vector2 tailleBasse;
    
    private bool retour;

    private void Start()
    {
        spriteRenderer.sprite = off;
        transform.localScale = tailleBasse;
    }

    void Update()
    {
        if (isMoving)
        {
            spriteRenderer.sprite = on;
            StartCoroutine(Move());
        }
        

    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        Transform childCol = col.collider.GetComponent<Transform>();
        childCol.localScale = new Vector3(1, 1, 1);
    }
    


    IEnumerator Move()
    {
        if (!retour) {
            
            transform.position = Vector2.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
            transform.localScale = Vector2.Lerp(transform.localScale, tailleBasse, growspeed * Time.deltaTime);

            if (Vector2.Distance (transform.position, pos1.position) < 0.05f)
            {
                transform.localScale = tailleBasse;
                yield return new WaitForSeconds(2f);
                retour = true;
            }
        }
        
        if (retour) {
            transform.position = Vector2.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
            transform.localScale = Vector2.Lerp(transform.localScale, tailleHaute, growspeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos2.position) < 0.05f) 
            {
                transform.localScale = tailleHaute;
                yield return new WaitForSeconds(2f);
                retour = false;
            }
        }
    }
}
