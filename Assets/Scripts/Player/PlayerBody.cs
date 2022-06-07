using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class PlayerBody : MonoBehaviour
{
    //=============================================================================================//
    //                                       -  VARIABLES  -                                       //
    //=============================================================================================//
    
    [Tooltip("The object where all the worm sprites are stored.")]
    [SerializeField] private WormSprites wormSprites;
    private WormSpritesStruct wormSpritesStruct;

    [Tooltip("If the player worm is supposed to be normal, bunker, ...")]
    [SerializeField] private WormType wormType;

    [Tooltip("The prefab of a body circle.")]
    [SerializeField] private GameObject bodyCircle;
    private GameObject followingBodyCircle;
    [SerializeField] private int bodyCirclesCount;

    private List<Vector2> positionList;

    //=============================================================================================//
    //                                         -  UNITY  -                                         //
    //=============================================================================================//

    private void Awake()
    {
        ErrorsCheck();
        BodySetup();
    }

    private void FixedUpdate()
    {
        if (followingBodyCircle)
        {
            followingBodyCircle.transform.rotation = transform.rotation;
            positionList.Add(transform.position);
            if (positionList.Count > bodyCirclesCount * 0.5f) // On peut controler la taille du ver avec cette ligne. A tester
            {
                positionList.RemoveAt(0);
                followingBodyCircle.transform.position = positionList[0];
            }
        }
    }

    //=============================================================================================//
    //                                      -  CUSTOM CODE  -                                      //
    //=============================================================================================//

    private void ErrorsCheck()
    {
        if (bodyCirclesCount <= 0)
        {
            throw new System.Exception($"PlayerBody.bodyCirclesCount is {bodyCirclesCount}. Please put a number above 0.");
        }

        if (!wormSprites)
        {
            throw new System.Exception("PlayerBody.wormSprites is null.");
        }

        if (!bodyCircle)
        {
            throw new System.Exception("PlayerBody.bodyCircle is null.");
        }

        bool wormTypeExists = false;
        foreach(var _struct in wormSprites.WormTypes)
        {
            if(_struct.WormType == wormType)
            {
                wormSpritesStruct = _struct;
                wormTypeExists = true;
            }

            if(!_struct.HeadSprite || !_struct.BodySprite)
            {
                throw new System.Exception("There are some missing sprites in the Worm Sprites object.");
            }
        }
        if (!wormTypeExists)
        {
            throw new System.Exception($"PlayerBody.wormType is {wormType}, but no element in the Worm Sprites object is of this type.");
        }
    }

    private void BodySetup()
    {
        SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
        mainSpriteRenderer.sprite = wormSpritesStruct.HeadSprite;
        for (int i = 0; i < bodyCirclesCount; i++)
        {
            GameObject bodyCircleClone = Instantiate(bodyCircle, transform.position, Quaternion.identity);
            if(i != bodyCirclesCount - 1)
            {
                bodyCircleClone.transform.localScale = transform.localScale * 0.9f;
            }
            else
            {
                bodyCircleClone.transform.localScale = transform.localScale * 0.75f;
            }
            bodyCircleClone.GetComponent<BodyBehaviour>().BodyCirclesCount = bodyCirclesCount;

            SpriteRenderer spriteRenderer = bodyCircleClone.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = wormSpritesStruct.BodySprite;
            spriteRenderer.sortingOrder = (mainSpriteRenderer.sortingOrder - 1) - i;

            if(i == 0)
            {
                followingBodyCircle = bodyCircleClone;
            }
            else
            {
                followingBodyCircle.GetComponent<BodyBehaviour>().AddFollowingBodyCircle(bodyCircleClone);
            }
        }

        positionList = new List<Vector2>();
    }
}