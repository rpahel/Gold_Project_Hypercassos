using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHoleColliders : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//

    [Range(0, 12), SerializeField]
    private int nombreDeTrous;
    [Range(0f, 1f), SerializeField]
    private float epaisseurSol;
    [SerializeField]
    private Color groundColor;

    [Header("Pas touche."), SerializeField]
    private Material groundMaterial;
    private List<GenerateCollider> grounds;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Awake()
    {
        // Populate Grounds list for to keep track of the grounds
        grounds = new List<GenerateCollider>();

        if(transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                var script = transform.GetChild(i).GetComponent<GenerateCollider>();
                script.EpaisseurSol = epaisseurSol;
                grounds.Add(script);
            }
        
            transform.parent.gameObject.GetComponent<LayerBehaviour>().Grounds = grounds;
        }
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    [ContextMenu("Generate Colliders")] 
    public void GenerateCollider()
    {
        if (transform.childCount > 0)
        {
            int count = transform.childCount;

            for(int i = count - 1; i >= 0; --i)
            {
                DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
            }
        }

        if(nombreDeTrous == 0)
        {
            GameObject collider = new GameObject($"Collider");
            collider.transform.SetParent(transform, false);
            GenerateCollider collScript = collider.AddComponent<GenerateCollider>();
            collScript.MaxPoints = 80;
            collScript.PercentageCircle = 100;
            collScript.GroundMaterial = groundMaterial;
            collScript.GroundColor = groundColor;
            collScript.EpaisseurSol = epaisseurSol;
            collScript.GenerateGroundCollision();
        }
        else
        {
            for(int i = 1; i < nombreDeTrous + 1; i++)
            {
                GameObject collider = new GameObject($"Collider {i}");
                collider.transform.SetParent(transform, false);
                GenerateCollider collScript = collider.AddComponent<GenerateCollider>();
                collScript.MaxPoints = 80;
                collScript.PercentageCircle = 100 / nombreDeTrous - 5;
                collScript.GroundMaterial = groundMaterial;
                collScript.GroundColor = groundColor;
                collScript.EpaisseurSol = epaisseurSol;
                collScript.GenerateGroundCollision();
                collider.transform.rotation = Quaternion.Euler(0, 0, (360f * (((float)i - 1f)/ (float)nombreDeTrous)) + transform.rotation.eulerAngles.z);
            }
        }
    }
}
