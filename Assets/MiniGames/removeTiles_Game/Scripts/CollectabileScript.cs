using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectabileScript : MonoBehaviour
{
    public int TilePos;
    [SerializeField]
    private GridManager gridManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
