using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create Level")]
public class GameLevel : ScriptableObject
{
    public GameObject[] levelLayers;
}