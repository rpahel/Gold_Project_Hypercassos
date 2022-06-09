using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerBehaviour : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Tooltip("Layer Parameters must go here."), SerializeField]
    private LayerParameters layerParameters;
    [Tooltip("The gameObject that will act as a veil to grey out the layer."), SerializeField]
    private GameObject greyed;
    [Tooltip("The gameObject that will act as a cover to hide the layer."), SerializeField]
    private GameObject cache;

    private SpriteRenderer greyedRenderer;
    private SpriteRenderer cacheRenderer;
    private AnimationCurve scaleCurve;
    private List<GenerateCollider> grounds;
    private GameObject[] boxes;
    private Vector3 targetScale;
    private Vector3 initialScale;
    private float iniGreyedAlpha;
    private float iniCacheAlpha;
    private bool scaling;

    // Properties
    public List<GenerateCollider> Grounds { get { return grounds; } set { grounds = value; } }
    public bool isScaling { get { return scaling; } }
    public float ScaleSpeed { get { return layerParameters.ScaleSpeed; } }

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Awake()
    {
        boxes = GameObject.FindGameObjectsWithTag("ExplosiveBox");
        ErrorsCheck();
        scaleCurve = layerParameters.ScaleCurve;
    }

    private void Start()
    {
        greyedRenderer = greyed.GetComponent<SpriteRenderer>();
        cacheRenderer = cache.GetComponent<SpriteRenderer>();
        iniGreyedAlpha = greyedRenderer.color.a;
        iniCacheAlpha = cacheRenderer.color.a;
        DisableBox();
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    private void ErrorsCheck()
    {
        if (!layerParameters)
        {
            throw new System.Exception("No Layer Parameters assigned to the Layer gameObject");
        }

        if (!greyed)
        {
            throw new System.Exception("No Greyed gameObject assigned to the Layer gameObject");
        }

        if (!cache)
        {
            throw new System.Exception("No Cache gameObject assigned to the Layer gameObject");
        }
    }

    public void DisableBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetActive(false);
        }
    }

    public void EnableBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetActive(true);
        }
    }

    ///<summary>
    /// Hides the layer (makes Cache appear).
    ///</summary>
    public void Hide()
    {
        StartCoroutine(Hiding());
    }

    private IEnumerator Hiding()
    {
        if (cache.activeSelf == true)
        {
            yield break;
        }

        cache.SetActive(true);

        for (float alpha = 0; alpha <= 1f; alpha += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp(0, iniCacheAlpha, alpha));
            yield return null;
        }

        yield break;
    }

    ///<summary>
    /// Reveals the layer (makes Cache go away).
    ///</summary>
    public void Discover()
    {
        StartCoroutine(Discovering());
    }

    private IEnumerator Discovering()
    {
        if (cache.activeSelf == false)
        {
            yield break;
        }

        for (float alpha = 0; alpha <= 1f; alpha += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp(iniCacheAlpha, 0, alpha));
            yield return null;
        }

        cache.SetActive(false);

        yield break;
    }

    ///<summary>
    /// Brings the layer into focus (ungreys it).
    ///</summary>
    public void Focus()
    {
        StartCoroutine(Focusing());
    }

    private IEnumerator Focusing()
    {
        if (greyed.activeSelf == false)
        {
            yield break;
        }

        for (float alpha = 0; alpha <= 1f; alpha += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(iniGreyedAlpha, 0, alpha));
            yield return null;
        }

        greyed.SetActive(false);

        yield break;
    }

    ///<summary>
    /// Hides the layer (greys it).
    ///</summary>
    public void GreyOut()
    {
        StartCoroutine(GreyingOut());
    }

    private IEnumerator GreyingOut()
    {
        if (greyed.activeSelf == true)
        {
            yield break;
        }

        greyed.SetActive(true);

        for (float alpha = 0; alpha <= 1f; alpha += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(0, iniGreyedAlpha, alpha));
            yield return null;
        }

        yield break;
    }

    ///<summary>
    /// Grows the layer to the desired scale.
    ///</summary>
    public void Grow(Vector3 scale)
    {
        scaling = true;
        initialScale = transform.localScale;
        targetScale = scale;
        StartCoroutine(Growing());
    }

    private IEnumerator Growing()
    {
        for(float l = 0; l <= 1f; l += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            float Beta = scaleCurve.Evaluate(l);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, Beta);
            yield return null;
        }

        scaling = false;

        if (grounds != null && grounds.Count > 0)
        {
            foreach (var ground in grounds)
            {
                ground.UpdateGroundWidth();
            }
        }

        yield break;
    }

    ///<summary>
    /// Shrinks the layer to the desired scale.
    ///</summary>
    public void Shrink(Vector3 scale)
    {
        scaling = true;
        initialScale = transform.localScale;
        targetScale = scale;
        StartCoroutine(Shrinking());
    }

    private IEnumerator Shrinking()
    {
        for (float l = 0; l <= 1f; l += Time.deltaTime * (1f / layerParameters.ScaleSpeed))
        {
            float Beta = scaleCurve.Evaluate(l);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, Beta);
            yield return null;
        }

        scaling = false;

        if (grounds != null && grounds.Count > 0)
        {
            foreach (var ground in grounds)
            {
                ground.UpdateGroundWidth();
            }
        }

        yield break;
    }

    ///<summary>
    /// Enable ground collisions.
    ///</summary>
    public void EnableGround()
    {
        if(grounds != null && grounds.Count > 0)
        {
            foreach (var ground in grounds)
            {
                ground.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }
    }

    ///<summary>
    /// Disable ground collisions.
    ///</summary>
    public void DisableGround()
    {
        if (grounds != null && grounds.Count > 0)
        {
            foreach (var ground in grounds)
            {
                ground.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            }
        }
    }

    ///<summary>
    /// Change ground z position.
    ///</summary>
    public void GroundZPos(float zPos)
    {
        if (grounds != null && grounds.Count > 0)
        {
            Transform obj = grounds[0].transform.parent;
            obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y, zPos);
        }
    }
}
