using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Tooltip("The maximum height the piston can go."), Range(0f, 8f)]
    [SerializeField] private float maxHeight;
    [Tooltip("The minimum height the piston can go."), Range(0f, 8f)]
    [SerializeField] private float minHeight;
    [Tooltip("Check this if you want the piston to go up first.")]
    [SerializeField] private bool upOrDown;
    [Tooltip("The speed of the piston."), Range(0.5f, 2f)]
    [SerializeField] private float travelSpeed;
    [Tooltip("The duration of the pause when the piston has reached target height."), Range(0f, 5f)]
    [SerializeField] private float waitTime;
    [Tooltip("How the piston will move")]
    [SerializeField] private AnimationCurve animationCurve;

    private Transform platform;
    private Transform rod;
    private Transform pistonBase;
    private Vector2 maxHeightVector;
    private Vector2 minHeightVector;
    private Vector2 maxHeightPosition;
    private Vector2 minHeightPosition;
    private float rodScale;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Start()
    {
        GetChildren();
        CalculateVectors();
        StartCoroutine(Movement());
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            if (minHeight >= maxHeight)
            {
                maxHeight = minHeight + .01f;
            }
        }
    
        GetChildren();

        if (!Application.isPlaying)
        {
            CalculateVectors();
            rod.localScale = new Vector3(rod.localScale.x, rodScale, 1f);
            platform.localPosition = maxHeightPosition;
        }
    
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pistonBase.position, pistonBase.up * maxHeight * transform.lossyScale.y);
        Gizmos.DrawWireSphere(pistonBase.position + pistonBase.up * maxHeight * transform.lossyScale.y, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(pistonBase.position, pistonBase.up * minHeight * transform.lossyScale.y);
        Gizmos.DrawWireSphere(pistonBase.position + pistonBase.up * minHeight * transform.lossyScale.y, 0.5f);
    }
    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    void GetChildren() // On récupère chaque enfant du l'objet piston
    {
        ErrorsCheck();

        platform = transform.GetChild(0);
        rod = transform.GetChild(1);
        pistonBase = transform.GetChild(2);
    }

    private void ErrorsCheck() // On regarde bien s'il n'y a que 3 enfants
    {
        if(transform.childCount != 3)
        {
            throw new System.Exception($"{name} has {transform.childCount} children. It should only have 3.");
        }
    }

    private void CalculateVectors() // On calcule la position min et max de la platform du piston, ainsi que le scale que la tige doit avoir.
    {
        maxHeightVector = Vector2.up * maxHeight;
        minHeightVector = Vector2.up * minHeight;
        maxHeightPosition = (Vector2)pistonBase.localPosition + maxHeightVector;
        minHeightPosition = (Vector2)pistonBase.localPosition + minHeightVector;
        rodScale = (platform.localPosition - pistonBase.localPosition).magnitude / 6.13f;
    }

    private IEnumerator Movement() // Coroutine qui s'appelle qui update la position de la platforme à chaque frame jusqu'à atteindre sa destination, puis rebelotte.
    {
        float alpha;

        if (upOrDown)
        {
            for(float f = 0f; f < 1f; f += Time.deltaTime * travelSpeed)
            {
                CalculateVectors();
                alpha = animationCurve.Evaluate(f);
                platform.localPosition = Vector2.Lerp(minHeightPosition, maxHeightPosition, alpha);
                rod.localScale = new Vector3(rod.localScale.x, rodScale, 1f);
                yield return null;
            }

            upOrDown = false;
        }
        else
        {
            for (float f = 0f; f < 1f; f += Time.deltaTime * travelSpeed)
            {
                CalculateVectors();
                alpha = animationCurve.Evaluate(f);
                platform.localPosition = Vector2.Lerp(maxHeightPosition, minHeightPosition, alpha);
                rod.localScale = new Vector3(rod.localScale.x, rodScale, 1f);
                yield return null;
            }

            upOrDown = true;
        }
        
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Movement());
        yield break;
    }
}
