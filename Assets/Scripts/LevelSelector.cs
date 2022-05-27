using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public int numberofLevels = 5;
    public Vector2 inconSpacing;

    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;
    private int nIcons;
    
    
    void Start()
    {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;

        int maxInARow = Mathf.FloorToInt((panelDimensions.width + inconSpacing.x)/(iconDimensions.width + inconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height +inconSpacing.y) / (iconDimensions.height + inconSpacing.y));

        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberofLevels/amountPerPage);
        LoadPanels(totalPages);
        
    }

    private void LoadPanels(int nPanels)
    {
        GameObject panelClone =  Instantiate(levelHolder) as GameObject;

        for (int i = 1; i <= nPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetupGrid(panel);
            nIcons = i == nPanels ? numberofLevels - currentLevelCount : amountPerPage;
            LoadIcons(ref nIcons, panel);
            Debug.Log(nIcons);
            
        }
        Destroy(panelClone);
    }

    private void SetupGrid(GameObject panel)
    {
       GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
       grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
       grid.childAlignment = TextAnchor.MiddleCenter;
       grid.spacing = inconSpacing;
    }
    

    protected virtual void LoadIcons(ref int nIcons, GameObject parentObject)
    {
        for (int i = 1; i <= nIcons; i++)
        {
            currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + currentLevelCount);
            
        }
    }

    

    
}
