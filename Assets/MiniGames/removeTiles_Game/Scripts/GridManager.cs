using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField]
    private GameObject TilePrefab;
    [SerializeField]
    private int Width, Height, TileCount_Pl1, TileCount_Pl2;
    [SerializeField]
    private Transform GridStart_PL1, GridStart_PL2;
    [SerializeField]
    private List<GameObject> NormalGrid_PL1, NormalGrid_PL2;

    [Header("SpawnCollectabile")]
    [SerializeField]
    private float SpawnCountdown, SpawnReset;
    [SerializeField]
    private GameObject CollectabilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGride();
    }

    private void GenerateGride()
    {
       
        
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var SpawnTile = Instantiate(TilePrefab, new Vector2(GridStart_PL1.position.x + x, GridStart_PL1.position.y + y), Quaternion.identity, GridStart_PL1);
                    SpawnTile.GetComponent<Tile>().Tilenum = TileCount_Pl1;
                    NormalGrid_PL1.Add(SpawnTile.gameObject);
                    SpawnTile.name = "Tile" + SpawnTile.GetComponent<Tile>().Tilenum;
                    TileCount_Pl1 += 1;
                }
            }
        

       
            for (int x = 0; x > -Width; x--)
            {
                for (int y = 0; y < Height; y++)
                {
                    var SpawnTile = Instantiate(TilePrefab, new Vector2(GridStart_PL2.position.x + x, GridStart_PL2.position.y + y), Quaternion.identity, GridStart_PL2);
                    SpawnTile.GetComponent<Tile>().Tilenum = TileCount_Pl2;
                    NormalGrid_PL2.Add(SpawnTile.gameObject);
                    SpawnTile.name = "Tile" + SpawnTile.GetComponent<Tile>().Tilenum;
                    TileCount_Pl2 += 1;
                }
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnCountdown <= 0)
        {
            SelectGrid_Pl1();
            SelectGrid_Pl2();
            SpawnCountdown = SpawnReset;
        }

        else
        {
            SpawnCountdown -= Time.deltaTime;
        }
    }

    private void SpawnCollectabile(GameObject SelectedTile)
    {
        var Col = Instantiate(CollectabilePrefab, SelectedTile.transform.position, Quaternion.identity, SelectedTile.transform);
        Col.gameObject.GetComponent<CollectabileScript>().TilePos = SelectedTile.GetComponent<Tile>().Tilenum;
        SelectedTile.gameObject.GetComponent<Tile>().hasCollectabile = true;
    }

    private void SelectGrid_Pl1()
    {
        int num_PL1 = UnityEngine.Random.Range(0, NormalGrid_PL1.Count);

        Tile SpawnPos1 = NormalGrid_PL1[num_PL1].gameObject.GetComponent<Tile>();

        if (SpawnPos1.hasCollectabile == true)
        {
            SelectGrid_Pl1();
        }

        else
        {
            SpawnCollectabile(SpawnPos1.gameObject);
        }
    }

    private void SelectGrid_Pl2()
    {
        int num_PL2 = UnityEngine.Random.Range(0, NormalGrid_PL2.Count);

        Tile SpawnPos = NormalGrid_PL2[num_PL2].gameObject.GetComponent<Tile>();

        if (SpawnPos.hasCollectabile == true)
        {
            SelectGrid_Pl2();
        }

        else
        {
            SpawnCollectabile(SpawnPos.gameObject);
        }
    }
}
