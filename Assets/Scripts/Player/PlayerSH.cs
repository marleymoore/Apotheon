using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSH : MonoBehaviour
{
    Vector2 pDirection;

    [SerializeField] SpriteRenderer pSpriteRenderer;


    [SerializeField] List<Sprite> nSprites; //north
    [SerializeField] List<Sprite> neSprites; //north east
    [SerializeField] List<Sprite> eSprites; //east
    [SerializeField] List<Sprite> seSprites; //south east
    [SerializeField] List<Sprite> sSprites; //south
    

    [SerializeField] float frameRate;
    float idleTime;

    
    // Start is called before the first frame update
    void Start()
    {
        pSpriteRenderer = GetComponent<SpriteRenderer>();

  
    }

    // Update is called once per frame
    void Update()
    {
        pDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //Console.WriteLine(pDirection);

        SpriteFlip();
        SetSprite();
      
    }

    void SpriteFlip()
    {
        pSpriteRenderer.flipX = pDirection.x < 0 ? true : false;

    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprite = null;


        selectedSprite = pDirection.y > 0 && Mathf.Abs(pDirection.x) > 0 ? neSprites : pDirection.y > 0 ? nSprites : selectedSprite; // if X axis and Y axis are positive choose neSprites, if only y axis then nSprites
        selectedSprite = pDirection.y < 0 && Mathf.Abs(pDirection.x) > 0 ? seSprites : pDirection.y < 0 ? sSprites : selectedSprite; // same as above but for seSprites and sSprites

        selectedSprite = Mathf.Abs(pDirection.x) > 0 && pDirection.y == 0 ? eSprites : selectedSprite; // if X axis is positive and players not moving on y axis then only eSprites

        return selectedSprite;


        // if (pDirection.y > 0)
        // {
        //     if (Mathf.Abs(pDirection.x) > 0)
        //     {
        //         selectedSprite = neSprites;
        //     }
        //     else
        //     {
        //         selectedSprite = nSprites;
        //     }
        // }
        // else if (pDirection.y < 0)
        // { 
        //     if(Mathf.Abs(pDirection.x) > 0)
        //     {
        //         selectedSprite = seSprites;
        //     }
        //     else
        //     {
        //         selectedSprite = sSprites;
        //     }
        // }
        // else
        // {
        //     if (Mathf.Abs(pDirection.x) > 0)
        //     {
        //         selectedSprite = eSprites;
        //     }
        // }

        
    }

    void SetSprite()
    {

        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime; // time since walk started
            int totalFrame = (int)(playTime * frameRate); // total frames passed since playing
            int frame = totalFrame % directionSprites.Count; // current frame

            pSpriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
            pSpriteRenderer.sprite = sSprites[0];
        }

    }

}
