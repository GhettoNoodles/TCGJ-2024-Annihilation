using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Tilenum;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite S_Normal, S_Burning, S_Destroyed;
    public TileState tileState;
    public bool hasCollectabile = false;
    
    
    public enum TileState
    {
        Normal,
        Burning,
        Destroyed
    }
    // Start is called before the first frame update
    void Start()
    {
        tileState = TileState.Normal;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetSprite();
    }

    private void SetSprite()
    {
        switch (tileState)
        {
            case TileState.Normal:
                spriteRenderer.sprite = S_Normal;
                break;
            case TileState.Burning:
                spriteRenderer.sprite = S_Burning;
                break;
            case TileState.Destroyed:
                spriteRenderer.sprite = S_Destroyed;
                break;
        }
    }
      
    
    // Update is called once per frame
        void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSprite();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tileState)
        {
            case TileState.Normal:
                
                break;
            case TileState.Burning:
                if (collision.gameObject.tag == "Player")
                {
                    Dead();
                }
                break;
            case TileState.Destroyed:
                if (collision.gameObject.tag == "Player")
                {
                    Dead();
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (tileState)
        {
            case TileState.Normal:

                break;
            case TileState.Burning:
                if (collision.gameObject.tag == "Player")
                {
                    Dead();
                }
                break;
            case TileState.Destroyed:
                if (collision.gameObject.tag == "Player")
                {
                    Dead();
                }
                break;
        }
    }

    private void Dead()
    {
        
    }
}