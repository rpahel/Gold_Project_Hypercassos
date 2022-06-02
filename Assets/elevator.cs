using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    
    

    public float speed = 2f;                                        
    [SerializeField, Range(0.1f, 50f)] private float pointA = 5f;   
    [SerializeField, Range(0.1f, 50f)] private float pointB = 5f;   
    [SerializeField, Range(0f, 360f)] private float RotationPath;   
    private Vector2 directionAngle;                                 
    private Vector3 pos1;                                 
    private Vector3 pos2;                                 
    private bool retour;                                            

    
    void Start() {
        directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
        pos1 = transform.position + (Vector3)directionAngle * pointA;
        pos2 = transform.position - (Vector3)directionAngle * pointB;
    }

    
    void Update() {
        
        if (!retour) {
            transform.position = Vector2.MoveTowards(transform.position, pos1, speed * Time.deltaTime);

            if (Vector2.Distance (transform.position, pos1) < 0.05f) {
                retour = true;
            }
        }
        
        if (retour) {
            transform.position = Vector2.MoveTowards(transform.position, pos2, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos2) < 0.05f) {
                retour = false;
            }
        }
    }

    
    void OnCollisionEnter2D(Collision2D truc) {
        if (truc.gameObject.tag == "Player") {
            truc.transform.parent = transform;
        }
    }

    
    void OnCollisionExit2D(Collision2D truc) {
        if (truc.gameObject.tag == "Player") {
            truc.transform.parent = null;
        }
    }

    void OnDrawGizmos() {
        if (!Application.IsPlaying(gameObject)) {
            directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
            pos1 = transform.position + (Vector3)directionAngle * pointA;
            pos2 = transform.position - (Vector3)directionAngle * pointB;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 0.2f);
        Gizmos.DrawSphere(pos2, 0.2f);
        Gizmos.DrawLine(pos1, pos2);
    }    
}
