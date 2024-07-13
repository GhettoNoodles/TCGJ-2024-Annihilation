using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public int Tilenum;
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] S_Normal, S_Burning;
    public TileState tileState;
    public bool hasCollectabile = false;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private float burningCountdown, burningReset;
    [SerializeField] private bool Player1Side;
    public GameObject CloneTile;

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
        spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SetSprite();
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        burningCountdown = burningReset;

        if (gameObject.transform.root == gridManager.GridStart_PL1)
        {
            Player1Side = true;
            CloneTile = gridManager.GridStart_PL2.GetChild(Tilenum).gameObject;
        }

        else
        {
            Player1Side = false;
            CloneTile = gridManager.GridStart_PL1.GetChild(Tilenum).gameObject;
        }
    }

    private void Awake()
    {
        
    }

    public void SetSprite()
    {
        switch (tileState)
        {

            case TileState.Normal:
                int num = UnityEngine.Random.Range(0, S_Normal.Length);
                spriteRenderer.sprite = S_Normal[num];
                spriteRenderer.color = Color.white;
                break;
            case TileState.Burning:
                burningCountdown = burningReset;
                int nommer = UnityEngine.Random.Range(0, S_Burning.Length);
                spriteRenderer.sprite = S_Burning[nommer];
                spriteRenderer.color = Color.white;
                if (transform.childCount > 1)
                {
                    Destroy(transform.GetChild(1).gameObject);
                }

                
                break;
            case TileState.Destroyed:
                int enigeIets = UnityEngine.Random.Range(0, S_Normal.Length);
                spriteRenderer.sprite = S_Normal[enigeIets];
                spriteRenderer.color = Color.grey;
                if (transform.childCount > 1)
                {
                    Destroy(transform.GetChild(1).gameObject);
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
                int nommer = UnityEngine.Random.Range(0, S_Burning.Length);
                spriteRenderer.sprite = S_Burning[nommer];
                spriteRenderer.color = Color.white;

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
                int enigeIets = UnityEngine.Random.Range(0, S_Normal.Length);
                spriteRenderer.sprite = S_Normal[enigeIets];
                spriteRenderer.color = Color.grey;
                break;
        }

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    switch (tileState)
    //    {
    //        case TileState.Normal:
                
    //            break;
    //        case TileState.Burning:
    //            if (collision.gameObject.tag == "Player1")
    //            {
    //                KillPlayer1();
    //            }

    //            else if (collision.gameObject.tag == "Player2")
    //            {
    //                KillPlayer2();
    //            }
    //            break;
    //        case TileState.Destroyed:
    //            if (collision.gameObject.tag == "Player1")
    //            {
    //                KillPlayer1();
    //            }

    //            else if (collision.gameObject.tag == "Player2")
    //            {
    //                KillPlayer2();
    //            }
    //            break;
    //    }
    //}

    

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    switch (tileState)
    //    {
    //        case TileState.Normal:

    //            break;
    //        case TileState.Burning:
    //            if (collision.gameObject.tag == "Player1")
    //            {
    //                KillPlayer1();
    //            }

    //            else if (collision.gameObject.tag == "Player2")
    //            {
    //                KillPlayer2();
    //            }
    //            break;
    //        case TileState.Destroyed:
    //            if (collision.gameObject.tag == "Player1")
    //            {
    //                KillPlayer1();
    //            }

    //            else if (collision.gameObject.tag == "Player2")
    //            {
    //                KillPlayer2();
    //            }
    //            break;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
        
    //}

    //private void KillPlayer1()
    //{
        
    //    gridManager.PL2_Scores();
    //}

    //private void KillPlayer2()
    //{
    //    gridManager.PL1_Scores();
    //}
}