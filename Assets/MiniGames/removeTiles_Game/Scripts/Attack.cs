using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<GameObject> TilesInRange;
    [SerializeField]
    private Sprite Highlight, normal;
    [SerializeField]
    private Transform Grid1, Grid2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor" &&
            collision.gameObject.GetComponent<Tile>().CloneTile.GetComponent<Tile>().tileState == Tile.TileState.Normal)
        {
            TilesInRange.Add(collision.gameObject);
            int num = collision.gameObject.GetComponent<Tile>().Tilenum;
            if (transform.parent.gameObject.tag == "Player1")
            {
                Grid2.GetChild(num).gameObject.GetComponent<Tile>().spriteRenderer.color = Color.red;
            }
            else if (transform.parent.gameObject.tag == "Player2")
            {
                Grid1.GetChild(num).gameObject.GetComponent<Tile>().spriteRenderer.color = Color.blue;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor" &&
            collision.gameObject.GetComponent<Tile>().CloneTile.GetComponent<Tile>().tileState != Tile.TileState.Normal)
        {
            TilesInRange.Remove(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {

           
            TilesInRange.Remove(collision.gameObject);
            int num = collision.gameObject.GetComponent<Tile>().Tilenum;
            if (transform.parent.gameObject.tag == "Player1")
            {
                Grid2.GetChild(num).gameObject.GetComponent<Tile>().spriteRenderer.color = Color.white;
            }
            else if (transform.parent.gameObject.tag == "Player2")
            {
               Grid1.GetChild(num).gameObject.GetComponent<Tile>().spriteRenderer.color = Color.white;
            }
        }
        
    }
}
