using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public GameLevel levelObject;
    private List<LayerBehaviour> levelLayers;

    [Tooltip("Spawns a layer every X seconds. Value MUST be greater than layers Scale Speed."), Range(0.1f, 2f)]
    public float layerSpawnRate;

    private int currentLayer;

    private void Start()
    {
        currentLayer = 1;
        levelLayers = new List<LayerBehaviour>();

        // Set each layer position and scale
        StartCoroutine(LayerSetup());
    }

    private void Update()
    {
        // Up a layer
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LayerUp();
        }

        // Down a layer
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //LayerDown();
        }
    }

    IEnumerator LayerSetup()
    {
        yield return new WaitForSeconds(0.1f);
        
        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            LayerBehaviour cloneLevelLayer = Instantiate(levelObject.levelLayers[layerIndex], gameObject.transform).GetComponent<LayerBehaviour>();
            cloneLevelLayer.transform.localPosition = new Vector3(0, 0, 1f * layerIndex);
            cloneLevelLayer.transform.localScale = new Vector3(0, 0, 1f);
            float scale = 0.676f * Mathf.Exp(0.394f * layerIndex);
            cloneLevelLayer.Grow(new Vector3(scale, scale, 1f));
            if (layerIndex != currentLayer)
            {
                cloneLevelLayer.GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                cloneLevelLayer.GetComponent<CircleCollider2D>().enabled = true;
            }

            levelLayers.Add(cloneLevelLayer);
            yield return new WaitForSeconds(layerSpawnRate);
        }

        for (int layerIndex = currentLayer + 1; layerIndex < levelLayers.Count; layerIndex++)
        {
            levelLayers[layerIndex].Hide();
        }

        levelLayers[0].GreyOut();
    }

    private void LayerUp()
    {

    }
}
