using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerBehaviour : MonoBehaviour
{
    public bool isDiscovered;
    public bool isGreyedOut;
    private Color iniColor;
    private new SpriteRenderer renderer;
    private Vector3 iniScale;
    public bool isGoingDown;
    public bool isGoingUp;
    private float alpha;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        iniColor = renderer.color;

        if (isGreyedOut)
        {
            renderer.color = Color.gray;
        }

        if (!isDiscovered)
        {
            renderer.color = Color.black;
        }

        iniScale = transform.localScale;
    }

    private void Update()
    {
        if (isGoingDown)
        {
            ScaleDownLerp(Time.deltaTime);
        }
        else if(isGoingUp)
        {
            ScaleUpLerp(Time.deltaTime);
        }

        
    }

    public void UnGreyOut()
    {
        renderer.color = iniColor;
        isGreyedOut = false;
    }

    public void GreyOut()
    {
        renderer.color = Color.gray;
        isGreyedOut = true;
    }

    public void Discover()
    {
        renderer.color = iniColor;
        isDiscovered = true;
    }

    public void ScaleDown()
    {
        isGoingDown = true;
    }

    public void ScaleUp()
    {
        isGoingUp = true;
    }

    private void ScaleDownLerp(float deltaTime)
    {
        alpha += deltaTime;
        alpha = Mathf.Clamp01(alpha);
        transform.localScale = Vector3.Lerp(iniScale, iniScale / 2f, alpha);
        if (transform.localScale == iniScale / 2f)
        {
            isGoingDown = false;
            alpha = 0;
            iniScale = transform.localScale;
        }
    }

    private void ScaleUpLerp(float deltaTime)
    {
        alpha += deltaTime;
        alpha = Mathf.Clamp01(alpha);
        transform.localScale = Vector3.Lerp(iniScale, iniScale * 2f, alpha);
        if (transform.localScale == iniScale * 2f)
        {
            isGoingUp = false;
            alpha = 0;
            iniScale = transform.localScale;
        }
    }
}
