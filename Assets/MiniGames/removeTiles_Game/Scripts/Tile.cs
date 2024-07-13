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
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private float burningCountdown, burningReset;
    
    
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
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        burningCountdown = burningReset;
    }

    public void SetSprite()
    {
        switch (tileState)
        {
            case TileState.Normal:
                spriteRenderer.sprite = S_Normal;
                break;
            case TileState.Burning:
                burningCountdown = burningReset;
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
        switch (tileState)
        {
            case TileState.Normal:
                
                break;
            case TileState.Burning:
                burningCountdown -= Time.deltaTime;

                if (burningCountdown <= 0)
                {
                    
                     if (transform.parent == gridManager.GridStart_PL1)
                    {
                        gridManager.NormalGrid_PL1.Add(gameObject);
                    }
                     else
                    {
                        gridManager.NormalGrid_PL2.Add(gameObject);
                    }

                    tileState = TileState.Normal;
                    SetSprite();
                }
                break;
            case TileState.Destroyed:
                
                break;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void KillPlayer1()
    {
        
        gridManager.PL2_Scores();
    }

    private void KillPlayer2()
    {
        gridManager.PL1_Scores();
    }
}