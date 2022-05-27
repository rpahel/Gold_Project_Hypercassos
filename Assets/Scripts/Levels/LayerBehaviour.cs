using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject greyed;
    [SerializeField] private GameObject cache;

    private SpriteRenderer greyedRenderer;
    private SpriteRenderer cacheRenderer;
    
    private float iniGreyedAlpha;
    private float iniCacheAlpha;
    private float alpha;

    private bool hiding;
    private bool discovering;
    private bool greying;
    private bool focusing;

    private void Start()
    {
        greyedRenderer = greyed.GetComponent<SpriteRenderer>();
        cacheRenderer = cache.GetComponent<SpriteRenderer>();
        iniGreyedAlpha = greyedRenderer.color.a;
        iniCacheAlpha = cacheRenderer.color.a;
    }

    private void FixedUpdate()
    {
        if (hiding)
        {
            Hiding();
        }

        if (discovering)
        {
            Discovering();
        }

        if (focusing)
        {
            Focusing();
        }

        if(greying)
        {
            GreyingOut();
        }
    }

    ///<summary>
    /// Hides the layer (makes Cache appear).
    ///</summary>
    public void Hide()
    {
        hiding = true;
        alpha = 0;
    }

    private void Hiding()
    {
        cache.SetActive(true);
        alpha += Time.fixedDeltaTime;
        alpha = Mathf.Clamp01(alpha);
        cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp( 0, iniCacheAlpha, alpha));
        if (cacheRenderer.color.a >= iniCacheAlpha)
        {
            hiding = false;
        }
    }

    ///<summary>
    /// Reveals the layer (makes Cache go away).
    ///</summary>
    public void Discover()
    {
        discovering = true;
        alpha = 0;
    }

    private void Discovering()
    {
        if(cache.activeSelf == false)
        {
            discovering = false;
            return;
        }

        alpha += Time.fixedDeltaTime;
        alpha = Mathf.Clamp01(alpha);
        cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp(iniCacheAlpha, 0, alpha));
        if(cacheRenderer.color.a <= 0)
        {
            discovering = false;
            cache.SetActive(false);
        }
    }

    ///<summary>
    /// Brings the layer into focus (ungreys it).
    ///</summary>
    public void Focus()
    {
        focusing = true;
        alpha = 0;
    }

    private void Focusing()
    {
        if(greyed.activeSelf == false)
        {
            focusing = false;
            return;
        }

        alpha += Time.fixedDeltaTime;
        alpha = Mathf.Clamp01(alpha);
        greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(iniGreyedAlpha, 0, alpha));
        if (greyedRenderer.color.a <= 0)
        {
            focusing = false;
            greyed.SetActive(false);
        }
    }

    ///<summary>
    /// Unhides the layer (greys it).
    ///</summary>
    public void GreyOut()
    {
        greying = true;
        alpha = 0;
    }

    private void GreyingOut()
    {
        greyed.SetActive(true);
        alpha += Time.fixedDeltaTime;
        alpha = Mathf.Clamp01(alpha);
        greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(0, iniGreyedAlpha, alpha));
        if (greyedRenderer.color.a >= iniGreyedAlpha)
        {
            greying = false;
        }
    }
}
