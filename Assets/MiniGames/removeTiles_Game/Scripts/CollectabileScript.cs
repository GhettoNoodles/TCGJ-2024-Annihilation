using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectabileScript : MonoBehaviour
{
    public int TilePos;
    [SerializeField]
    private GridManager gridManager;
    public bool Player1Side;
    public GameObject MirrorTile;
    
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();

        if (Player1Side == true)
        {
            MirrorTile = gridManager.GridStart_PL2.GetChild(TilePos).gameObject;
        }

        else
        {
            MirrorTile = gridManager.GridStart_PL1.GetChild(TilePos).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MirrorTile.GetComponent<Tile>().tileState != Tile.TileState.Normal)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            gridManager.DestroyTile_PL1(TilePos);
        }

        else if (collision.gameObject.tag == "Player2")
        {
            gridManager.DestroyTile_PL2(TilePos);
        }

        Destroy(gameObject);
    }
}
