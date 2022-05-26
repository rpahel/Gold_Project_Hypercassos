using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public int currentLayer;
    public int previousLayer;

    public LayerBehaviour[] layers;

    private void Start()
    {
        previousLayer = currentLayer;
    }

    [ContextMenu("Layer Down")]
    public void LayerDown()
    {
        currentLayer--;
        ChangeLayer(currentLayer);
    }

    [ContextMenu("Layer Up")]
    public void LayerUp()
    {
        currentLayer++;
        ChangeLayer(currentLayer);
    }

    private void ChangeLayer(int layer)
    {
        if(currentLayer > previousLayer)
        {
            foreach(var layerScript in layers)
            {
                layerScript.ScaleDown();
            }
        }
        else if(currentLayer <= previousLayer)
        {
            foreach (var layerScript in layers)
            {
                layerScript.ScaleUp();
            }
        }

        layers[currentLayer].Discover();
        layers[currentLayer].UnGreyOut();

        for (int i = 0; i < layers.Length; i++)
        {
            if (i != currentLayer && layers[i].isDiscovered == true)
            {
                layers[i].GreyOut();
            }
        }

        previousLayer = currentLayer;
    }
}
