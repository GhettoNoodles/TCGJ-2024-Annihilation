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

    public void SetSprite()
    {
        switch (tileState)
        {
            case TileState.Normal:
                spriteRenderer.sprite = S_Normal;
                break;
            case TileState.Burning:
                spriteRenderer.sprite = S_Burning;
                if (transform.childCount > 0)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
                break;
            case TileState.Destroyed:
                spriteRenderer.sprite = S_Destroyed;

                if (transform.childCount > 0)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
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
                if (collision.gameObject.tag == "Player1")
                {
                    KillPlayer1();
                }

                else if (collision.gameObject.tag == "Player2")
                {
                    KillPlayer2();
                }
                break;
            case TileState.Destroyed:
                if (collision.gameObject.tag == "Player1")
                {
                    KillPlayer1();
                }

                else if (collision.gameObject.tag == "Player2")
                {
                    KillPlayer2();
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
                if (collision.gameObject.tag == "Player1")
                {
                    KillPlayer1();
                }

                else if (collision.gameObject.tag == "Player2")
                {
                    KillPlayer2();
                }
                break;
            case TileState.Destroyed:
                if (collision.gameObject.tag == "Player1")
                {
                    KillPlayer1();
                }

                else if (collision.gameObject.tag == "Player2")
                {
                    KillPlayer2();
                }
                break;
        }
    }

    private void KillPlayer1()
    {
        Debug.Log("Player 2 Wins");
    }

    private void KillPlayer2()
    {
        Debug.Log("Player 1 Wins");
    }
}