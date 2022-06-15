using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public enum WormType
    {
        Normal,
        Bunker
    }


    [Serializable]
    public struct WormSpritesStruct
    {
        [SerializeField] private WormType wormType;
        [SerializeField] private Sprite headSprite;
        [SerializeField] private Sprite bodySprite;

        public WormType WormType { get { return wormType; } }
        public Sprite HeadSprite { get { return headSprite; } }
        public Sprite BodySprite { get { return bodySprite; } }
    }
}
