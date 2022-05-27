using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public GameLevel levelObject;
    private List<GameObject> levelLayers;
    public GameObject currentLayerObj;

    private int currentLayer;

    private float alpha;
    private float scale;

    private bool growing;

    private void Start()
    {
        currentLayer = 1;
        levelLayers = new List<GameObject>();

        // Set each layer position and scale
        StartCoroutine(LayerSetup());
    }

    private void FixedUpdate()
    {
        if (currentLayerObj && growing)
        {
            alpha += Time.fixedDeltaTime * 5f;
            alpha = Mathf.Clamp01(alpha);
            currentLayerObj.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(scale, scale, 1f), alpha);
        }
    }

    private void Update()
    {
        // Up a layer
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }

        // Down a layer
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
    }

    IEnumerator LayerSetup()
    {
        growing = true;

        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject cloneLevelLayer = Instantiate(levelObject.levelLayers[layerIndex], gameObject.transform);
            cloneLevelLayer.transform.localScale = new Vector3(0, 0, 1f);
            alpha = 0;
            currentLayerObj = cloneLevelLayer;
            scale = 0.676f * Mathf.Exp(0.394f * layerIndex);
            cloneLevelLayer.transform.localPosition = new Vector3(0, 0, 1f * layerIndex);
            if (layerIndex != currentLayer)
            {
                cloneLevelLayer.GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                cloneLevelLayer.GetComponent<CircleCollider2D>().enabled = true;
            }

            levelLayers.Add(cloneLevelLayer);
        }

        for (int layerIndex = currentLayer + 1; layerIndex < levelLayers.Count; layerIndex++)
        {
            levelLayers[layerIndex].GetComponent<LayerBehaviour>().Hide();
        }

        growing = false;
        currentLayerObj = null;
    }
}
