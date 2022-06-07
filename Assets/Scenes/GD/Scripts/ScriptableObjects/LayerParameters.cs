using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "DONT TOUCH/Create LayerParameters")]
public class LayerParameters : ScriptableObject
{
    [Tooltip("Time it takes for the layer to scale to its desired size. Value MUST be lower than Layer Spawn Rate."), Range(0.1f, 2f), SerializeField]
    private float scaleSpeed;
    private float alpha;
    private float beta;
    [Tooltip("The way the layers scale."), SerializeField]
    private AnimationCurve scaleCurve;

    public float ScaleSpeed { get { return scaleSpeed; } }
    public float Alpha { get { return alpha; } set { alpha = value; } }
    public float Beta { get { return beta; } set { beta = value; } }
    public AnimationCurve ScaleCurve { get { return scaleCurve; } }
}