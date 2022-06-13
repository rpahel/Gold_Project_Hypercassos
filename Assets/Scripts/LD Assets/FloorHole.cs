using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHole : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [SerializeField, Range(0f, 4f)]
    private float targetPositionFromAbove;
    [SerializeField, Range(0f, 4f)]
    private float targetPositionFromBelow;
    [SerializeField, Range(0f, 2f)]
    private float cooldownTimer;

    private LevelBehaviour levelBehaviour;
    private LayerBehaviour layerBehaviour;
    private BoxCollider2D coll;
    private Vector2 above;
    private Vector2 below;
    private float scaleSpeed;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Start()
    {
        levelBehaviour = FindObjectOfType<LevelBehaviour>();
        layerBehaviour = FindObjectOfType<LayerBehaviour>();
        coll = GetComponent<BoxCollider2D>();
        GetComponent<SpriteRenderer>().enabled = false;
        scaleSpeed = layerBehaviour.ScaleSpeed;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            CalculateTargetRanges();

            if (FromAbove(collider.transform.position))
            {
                StartCoroutine(PosTranslation(collider.transform, transform.up * targetPositionFromAbove));
                levelBehaviour.RequestLayerDown();
            }
            else
            {
                StartCoroutine(PosTranslation(collider.transform, -transform.up * targetPositionFromBelow));
                levelBehaviour.RequestLayerUp();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        CalculateTargetRanges();
        Gizmos.color = Color.green;
        Gizmos.DrawRay(below, transform.right * transform.localScale.x * 0.5f);
        Gizmos.DrawRay(below, -transform.right * transform.localScale.x * 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(above, transform.right * transform.localScale.x * 0.5f);
        Gizmos.DrawRay(above, -transform.right * transform.localScale.x * 0.5f);
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    private void CalculateTargetRanges()
    {
        below = transform.position - transform.up * targetPositionFromBelow;
        above = transform.position + transform.up * targetPositionFromAbove;
    }

    private bool FromAbove(Vector2 pos)
    {
        Vector2 thisToPos = pos - (Vector2)transform.position;
        float dot = Vector2.Dot(thisToPos, transform.up);

        if(dot > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator PosTranslation(Transform t, Vector3 dir)
    {
        coll.isTrigger = false;

        Vector2 ini = t.position;
        Vector2 target = t.position + dir;
        for(float alpha = 0f; alpha < 1f; alpha += Time.deltaTime * (1 / scaleSpeed))
        {
            t.position = Vector3.Lerp(ini, target, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(cooldownTimer);

        coll.isTrigger = true;

        yield break;
    }
}
