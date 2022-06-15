using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Tooltip("Your level must go here."), SerializeField]
    private GameLevel levelObject;
    [Tooltip("Spawns a layer every X seconds. Value MUST be greater than layers Scale Speed."), Range(0.1f, 2f), SerializeField]
    private float layerSpawnRate;

    private int currentLayer;
    private Player player;
    private List<LayerBehaviour> levelLayers;

    // Properties
    public int CurrentLayer { get { return currentLayer; } }
    public List<LayerBehaviour> LevelLayers { get { return levelLayers; } }

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Awake()
    {
        ErrorsCheck();
    }

    private void Start()
    {
        currentLayer = 1;
        levelLayers = new List<LayerBehaviour>();
        player = FindObjectOfType<Player>();

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

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    private void ErrorsCheck()
    {
        if (!levelObject)
        {
            throw new System.Exception("No Level Object assigned to the Level gameObject");
        }

        if (levelObject.levelLayers.Length == 0)
        {
            throw new System.Exception("No Level Layers in the levelLayers array of the assigned Level Object.");
        }
    }

    IEnumerator LayerSetup()
    {
        player.Freeze();

        yield return new WaitForSeconds(0.5f);

        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            SpawnLevelLayer(layerIndex);
            yield return new WaitForSeconds(layerSpawnRate);
        }

        SetupHideLayers();

        // Enable everything after layer setup
        float wait = 0f;
        for (int layerIndex = 0; layerIndex < levelObject.levelLayers.Length; layerIndex++)
        {
            levelLayers[layerIndex].EnableGround();
            wait = levelLayers[layerIndex].ScaleSpeed;
        }
        yield return new WaitForSeconds(wait);
        player.UnFreeze();
        foreach (LayerBehaviour item in levelLayers)
        {
            item.EnableBox();
        }
    }

    private void SpawnLevelLayer(int layerIndex)
    {
        LayerBehaviour cloneLevelLayer = Instantiate(levelObject.levelLayers[layerIndex], gameObject.transform).GetComponent<LayerBehaviour>();
        cloneLevelLayer.transform.localPosition = new Vector3(0, 0, 1f * layerIndex);
        cloneLevelLayer.transform.localScale = new Vector3(0, 0, 1f);
        cloneLevelLayer.DisableGround();
        cloneLevelLayer.DisableBox();
        float scale = 0.676f * Mathf.Exp(0.394f * layerIndex);
        cloneLevelLayer.Grow(new Vector3(scale, scale, 1f));

        levelLayers.Add(cloneLevelLayer);
    }

    private void SetupHideLayers()
    {
        for (int layerIndex = currentLayer + 1; layerIndex < levelLayers.Count; layerIndex++)
        {
            if (layerIndex == currentLayer + 1)
            {
                levelLayers[layerIndex].GroundZPos(-1.01f);
            }

            levelLayers[layerIndex].Hide();
        }

        levelLayers[currentLayer].GroundZPos(-1.51f);
        levelLayers[0].GreyOut();
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

        player.Freeze();

        // For each layer, shrinks it and discover it/focus it/hides it if necessary
        for(int l = 0; l < levelLayers.Count; l++)
        {
            float scale = 0.676f * Mathf.Exp(0.394f * (l - (currentLayer - 1)));
            
            levelLayers[l].Shrink(new Vector3(scale, scale, 1f));

            if (l != currentLayer)
            {
                levelLayers[l].GreyOut();
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
            }

            timeToWait = levelLayers[l].ScaleSpeed;
        }

        yield return new WaitForSeconds(timeToWait);

        player.UnFreeze();

        yield break;
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

        player.Freeze();

        // For each layer, grows it and discover it/focus it/hides it if necessary
        for (int l = 0; l < levelLayers.Count; l++)
        {
            float scale = 0.676f * Mathf.Exp(0.394f * (l - (currentLayer - 1)));

            levelLayers[l].Grow(new Vector3(scale, scale, 1f));

            if (l != currentLayer)
            {
                levelLayers[l].GreyOut();
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

        yield return new WaitForSeconds(timeToWait);

        player.UnFreeze();

        yield break;
    }
}
