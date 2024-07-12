using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<GameObject> TilesInRange;
    
    
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
            collision.gameObject.GetComponent<Tile>().tileState == Tile.TileState.Normal)
        {
            TilesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor" &&
            collision.gameObject.GetComponent<Tile>().tileState == Tile.TileState.Normal)
        {
            TilesInRange.Remove(collision.gameObject);
        }
        
    }
}
