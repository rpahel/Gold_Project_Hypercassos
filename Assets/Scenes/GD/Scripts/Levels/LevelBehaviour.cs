using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [Tooltip("Your level must go here.")]
    public GameLevel levelObject;
    private List<LayerBehaviour> levelLayers;

    [Tooltip("Spawns a layer every X seconds. Value MUST be greater than layers Scale Speed."), Range(0.1f, 2f)]
    public float layerSpawnRate;

    private int currentLayer;
    private ASTITOUCH asticul;

    private void Awake()
    {
        if (!levelObject)
        {
            throw new System.Exception("No Level Object assigned to the Level gameObject");
        }

        if(levelObject.levelLayers.Length == 0)
        {
            throw new System.Exception("No Level Layers in the levelLayers array of the assigned Level Object.");
        }

        asticul = FindObjectOfType<ASTITOUCH>();
    }

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
            RequestLayerUp();
        }

        // Down a layer
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RequestLayerDown();
        }
    }

    IEnumerator LayerSetup()
    {
        asticul.Freeze();
        yield return new WaitForSeconds(0.5f);
        
        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            LayerBehaviour cloneLevelLayer = Instantiate(levelObject.levelLayers[layerIndex], gameObject.transform).GetComponent<LayerBehaviour>();
            cloneLevelLayer.transform.localPosition = new Vector3(0, 0, 1f * layerIndex);
            cloneLevelLayer.transform.localScale = new Vector3(0, 0, 1f);
            float scale = 0.676f * Mathf.Exp(0.394f * layerIndex);
            cloneLevelLayer.DisableGround();
            cloneLevelLayer.Grow(new Vector3(scale, scale, 1f));
            cloneLevelLayer.DisableBox();
            levelLayers.Add(cloneLevelLayer);
            
            yield return new WaitForSeconds(layerSpawnRate);

        }

        for (int layerIndex = currentLayer + 1; layerIndex < levelLayers.Count; layerIndex++)
        {
            if(layerIndex == currentLayer + 1)
            {
                levelLayers[layerIndex].GroundZPos(-1.01f);
            }
            levelLayers[layerIndex].Hide();
        }

        levelLayers[currentLayer].GroundZPos(-1.51f);
        levelLayers[0].GreyOut();
        float wait = 0f;
        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            levelLayers[layerIndex].EnableGround();
            wait = levelLayers[layerIndex].ScaleSpeed;
        }

        yield return new WaitForSeconds(wait);
        asticul.UnFreeze();
        
        foreach (LayerBehaviour item in levelLayers)
        {
            item.EnableBox();
        }
        
    }

    public void RequestLayerUp()
    {
        if (currentLayer == levelLayers.Count - 1)
        {
            Debug.Log("You are at the top layer.");
            return;
        }

        if (levelLayers[0].isScaling)
        {
            Debug.Log("The world is scaling. Please wait and try again.");
        }
        else
        {
            StartCoroutine(LayerUp());
        }
    }

    private IEnumerator LayerUp()
    {
        float timeToWait = 1f;
        currentLayer++;

        asticul.Freeze();
        for(int l = 0; l < levelLayers.Count; l++)
        {
            float scale = 0.676f * Mathf.Exp(0.394f * (l - (currentLayer - 1)));
            
            levelLayers[l].Shrink(new Vector3(scale, scale, 1f));

            if (l != currentLayer)
            {
                levelLayers[l].GreyOut();
                //levelLayers[l].DisableGround();
                if (l == currentLayer + 1)
                {
                    levelLayers[l].GroundZPos(-1.01f);
                }
                else if (l == currentLayer - 1)
                {
                    levelLayers[l].GroundZPos(-1.01f);
                }
                else
                {
                    levelLayers[l].GroundZPos(0f);
                }
            }
            else
            {
                levelLayers[l].Discover();
                levelLayers[l].Focus();
                levelLayers[l].EnableGround();
                levelLayers[l].GroundZPos(-1.51f);
                //levelLayers[l].EnableColi();
            }

            timeToWait = levelLayers[l].ScaleSpeed;
        }

        yield return new WaitForSeconds(timeToWait * 0.25f);

        asticul.UnFreeze();
    }

    public void RequestLayerDown()
    {
        if (currentLayer == 1)
        {
            Debug.Log("You are at the bottom layer.");
            return;
        }

        if (levelLayers[0].isScaling)
        {
            Debug.Log("The world is scaling. Please wait and try again.");
        }
        else
        {
            StartCoroutine(LayerDown());
        }
    }

    private IEnumerator LayerDown()
    {
        float timeToWait = 1f;
        currentLayer--;

        asticul.Freeze();
        for (int l = 0; l < levelLayers.Count; l++)
        {
            float scale = 0.676f * Mathf.Exp(0.394f * (l - (currentLayer - 1)));

            levelLayers[l].Grow(new Vector3(scale, scale, 1f));

            if (l != currentLayer)
            {
                levelLayers[l].GreyOut();
                //levelLayers[l].DisableGround();
                if (l == currentLayer + 1)
                {
                    levelLayers[l].GroundZPos(-1.01f);
                }
                else if(l == currentLayer - 1)
                {
                    levelLayers[l].GroundZPos(-1.01f);
                }
                else
                {
                    levelLayers[l].GroundZPos(0f);
                }
            }
            else
            {
                levelLayers[l].Discover();
                levelLayers[l].Focus();
                levelLayers[l].EnableGround();
                levelLayers[l].GroundZPos(-1.51f);
            }

            timeToWait = levelLayers[l].ScaleSpeed;
        }

        yield return new WaitForSeconds(timeToWait * 0.25f);

        asticul.UnFreeze();
        
    }
}