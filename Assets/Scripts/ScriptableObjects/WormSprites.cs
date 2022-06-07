using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

[CreateAssetMenu(fileName = "Worm Sprites", menuName = "DONT TOUCH/Create WormSprites")]
public class WormSprites : ScriptableObject
{
    [SerializeField] private WormSpritesStruct[] wormTypes;
    public WormSpritesStruct[] WormTypes { get { return wormTypes; } }
}