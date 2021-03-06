using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "DONT TOUCH/Create LayerParameters")]
public class LayerParameters : ScriptableObject
{
    [Tooltip("Time it takes for the layer to scale to its desired size. Value MUST be lower than Layer Spawn Rate."), Range(0.1f, 0.5f), SerializeField]
    private float scaleSpeed;
    [Tooltip("The way the layers scale."), SerializeField]
    private AnimationCurve scaleCurve;

    public float ScaleSpeed { get { return scaleSpeed; } }
    public AnimationCurve ScaleCurve { get { return scaleCurve; } }
}