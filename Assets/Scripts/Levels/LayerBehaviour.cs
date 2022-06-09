using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerBehaviour : MonoBehaviour
{
    [Tooltip("Layer Parameters must go here."), SerializeField]
    private LayerParameters layerParameters;

    [Tooltip("The gameObject that will act as a veil to grey out the layer.")]
    [SerializeField] private GameObject greyed;
    [Tooltip("The gameObject that will act as a cover to hide the layer.")]
    [SerializeField] private GameObject cache;

    private SpriteRenderer greyedRenderer;
    private SpriteRenderer cacheRenderer;

    private AnimationCurve scaleCurve;

    private List<GenerateCollider> grounds;
    public List<GenerateCollider> Grounds { get { return grounds; } set { grounds = value; } }

    private Vector3 targetScale;
    private Vector3 initialScale;

    private float iniGreyedAlpha;
    private float iniCacheAlpha;
    public float ScaleSpeed { get { return layerParameters.ScaleSpeed; } }

    private bool hiding;
    private bool discovering;
    private bool greying;
    private bool focusing;
    private bool growing;
    private bool shrinking;
    public bool isScaling { get { return (shrinking || growing); } }

    public GameObject[] box;
    public GameObject[] explosivebox;

    


    //private LevelBehaviour level;
    //private CircleCollider2D trigger;
    private void Awake()
    {
        
        box= GameObject.FindGameObjectsWithTag("ExplosiveBox");
        explosivebox = GameObject.FindGameObjectsWithTag("Box");





        //level = FindObjectOfType<LevelBehaviour>();
        //trigger=GetComponent<CircleCollider2D>();
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

        scaleCurve = layerParameters.ScaleCurve;
    }

    public void DisableBox()
    {

        for (int i = 0; i < box.Length; i++)
        {
            box[i].SetActive(false);
            
        }
        for (int i = 0; i < explosivebox.Length; i++)
        {
            box[i].SetActive(false);

        }
    }
    //faire une fonction qui active les boxs
    public void EnableBox()
    {
        for (int i = 0; i < box.Length; i++)
        {
            box[i].SetActive(true);
        }
        for (int i = 0; i < explosivebox.Length; i++)
        {
            box[i].SetActive(true);

        }
    }
    //couroutine qui fait apparaitre les boxs

    private void Start()
    {
        greyedRenderer = greyed.GetComponent<SpriteRenderer>();
        cacheRenderer = cache.GetComponent<SpriteRenderer>();
        iniGreyedAlpha = greyedRenderer.color.a;
        iniCacheAlpha = cacheRenderer.color.a;
        DisableBox();
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

        if (greying)
        {
            GreyingOut();
            
        }

        if (growing)
        {
            Growing();
            
        }

        if (shrinking)
        {
            Shrinking();
            
        }
    }

    ///<summary>
    /// Hides the layer (makes Cache appear).
    ///</summary>
    public void Hide()
    {
        
        hiding = true;
        layerParameters.Alpha = 0;
    }

    private void Hiding()
    {
        
        cache.SetActive(true);
        layerParameters.Alpha += Time.fixedDeltaTime;
        layerParameters.Alpha = Mathf.Clamp01(layerParameters.Alpha);
        cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp( 0, iniCacheAlpha, layerParameters.Alpha));
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
        layerParameters.Alpha = 0;
    }

    private void Discovering()
    {
        if(cache.activeSelf == false)
        {
            discovering = false;
            return;
        }

        layerParameters.Alpha += Time.fixedDeltaTime;
        layerParameters.Alpha = Mathf.Clamp01(layerParameters.Alpha);
        cacheRenderer.color = new Color(cacheRenderer.color.r, cacheRenderer.color.g, cacheRenderer.color.b, Mathf.Lerp(iniCacheAlpha, 0, layerParameters.Alpha));
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
        layerParameters.Alpha = 0;
    }

    private void Focusing()
    {
        if(greyed.activeSelf == false)
        {
            focusing = false;
            return;
        }

        layerParameters.Alpha += Time.fixedDeltaTime;
        layerParameters.Alpha = Mathf.Clamp01(layerParameters.Alpha);
        greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(iniGreyedAlpha, 0, layerParameters.Alpha));
        if (greyedRenderer.color.a <= 0)
        {
            focusing = false;
            greyed.SetActive(false);
        }
    }

    ///<summary>
    /// Hides the layer (greys it).
    ///</summary>
    public void GreyOut()
    {
        greying = true;
        layerParameters.Alpha = 0;
    }

    private void GreyingOut()
    {
        greyed.SetActive(true);
        layerParameters.Alpha += Time.fixedDeltaTime;
        layerParameters.Alpha = Mathf.Clamp01(layerParameters.Alpha);
        greyedRenderer.color = new Color(greyedRenderer.color.r, greyedRenderer.color.g, greyedRenderer.color.b, Mathf.Lerp(0, iniGreyedAlpha, layerParameters.Alpha));
        if (greyedRenderer.color.a >= iniGreyedAlpha)
        {
            greying = false;
        }
    }

    ///<summary>
    /// Grows the layer to the desired scale.
    ///</summary>
    public void Grow(Vector3 scale)
    {
        growing = true;
        layerParameters.Beta = 0;
        initialScale = transform.localScale;
        targetScale = scale;
    }

    private void Growing()
    {
        layerParameters.Beta += Time.fixedDeltaTime * (1f/ layerParameters.ScaleSpeed);
        layerParameters.Beta = Mathf.Clamp01(layerParameters.Beta);
        float Beta = scaleCurve.Evaluate(layerParameters.Beta);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, Beta);
        if(transform.localScale.sqrMagnitude >= targetScale.sqrMagnitude)
        {
            growing = false;
        }
        foreach (var ground in grounds)
        {
            ground.UpdateGroundWidth();
        }

    }

    ///<summary>
    /// Shrinks the layer to the desired scale.
    ///</summary>
    public void Shrink(Vector3 scale)
    {
        shrinking = true;
        layerParameters.Beta = 0;
        initialScale = transform.localScale;
        targetScale = scale;
    }

    private void Shrinking()
    {
        layerParameters.Beta += Time.fixedDeltaTime * (1f / layerParameters.ScaleSpeed);
        layerParameters.Beta = Mathf.Clamp01(layerParameters.Beta);
        float Beta = scaleCurve.Evaluate(layerParameters.Beta);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, Beta);
        if (transform.localScale.sqrMagnitude <= targetScale.sqrMagnitude)
        {
            shrinking = false;
        }

        foreach(var ground in grounds)
        {
            ground.UpdateGroundWidth();
        }
    }

    ///<summary>
    /// Enable ground collisions.
    ///</summary>
    public void EnableGround()
    {
        foreach(var ground in grounds)
        {
            ground.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        }
    }

    ///<summary>
    /// Disable ground collisions.
    ///</summary>
    public void DisableGround()
    {
        foreach (var ground in grounds)
        {
            ground.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            
        }
    }

    ///<summary>
    /// Change ground z position.
    ///</summary>
    public void GroundZPos(float zPos)
    {
        Transform obj = grounds[0].transform.parent;
        obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y, zPos);
    }
}
