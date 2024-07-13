using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField]
    private GameObject TilePrefab;
    [SerializeField]
    private int Width, Height, TileCount_Pl1, TileCount_Pl2, MinWidth, MinHeight;
    public Transform GridStart_PL1, GridStart_PL2;
    public List<GameObject> NormalGrid_PL1, NormalGrid_PL2;
    [SerializeField]
    private bool DecreaseMap, MinSizereached;

    [Header("SpawnCollectabile")]
    [SerializeField]
    private float SpawnCountdown, SpawnReset;
    [SerializeField]
    private GameObject CollectabilePrefab;

    [Header("Score")]
    [SerializeField]
    public int Score_PL1, Score_PL2;
    [SerializeField]
    private TextMeshProUGUI TextScore1, TextScore2;
    [SerializeField]
    private PlayerController_RT PLC1, PLC2;

    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGride();
        SpawnCountdown = SpawnReset;
        TextScore1.text = Score_PL1.ToString();
        TextScore2.text = Score_PL2.ToString();
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
        
        if (Col.gameObject.transform.root == GridStart_PL1)
        {
            Col.gameObject.GetComponent<CollectabileScript>().Player1Side = true;
        }

        else
        {
            Col.gameObject.GetComponent<CollectabileScript>().Player1Side = false;
        }
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

    public void DestroyTile_PL1(int num)
    {
        GameObject destroyTile = GridStart_PL2.transform.GetChild(num).gameObject;
        NormalGrid_PL2.Remove(destroyTile);
        destroyTile.GetComponent<Tile>().tileState = Tile.TileState.Destroyed;
        destroyTile.GetComponent<Tile>().SetSprite();

    }

    public void DestroyTile_PL2(int num)
    {
        GameObject destroyTile = GridStart_PL1.transform.GetChild(num).gameObject;
        NormalGrid_PL1.Remove(destroyTile);
        destroyTile.GetComponent<Tile>().tileState = Tile.TileState.Destroyed;
        destroyTile.GetComponent<Tile>().SetSprite();
    }

    public void SetTileOnFire_PL1(int num)
    {
        GameObject destroyTile = GridStart_PL2.transform.GetChild(num).gameObject;
        NormalGrid_PL2.Remove(destroyTile);
        destroyTile.GetComponent<Tile>().tileState = Tile.TileState.Burning;
        destroyTile.GetComponent<Tile>().SetSprite();
    }

    public void SetTileOnFire_PL2(int num)
    {
        GameObject destroyTile = GridStart_PL1.transform.GetChild(num).gameObject;
        NormalGrid_PL1.Remove(destroyTile);
        destroyTile.GetComponent<Tile>().tileState = Tile.TileState.Burning;
        destroyTile.GetComponent<Tile>().SetSprite();
    }

    public void PL1_Scores()
    {
        Score_PL1 += 1;
        GameReset();
    }

    public void PL2_Scores()
    {
        Score_PL2 += 1;
        GameReset();
    }

    private void GameReset()
    {
        TextScore1.text = Score_PL1.ToString();
        TextScore2.text = Score_PL2.ToString();

        for (int i = 0; i < GridStart_PL1.childCount; i++)
        {
            GridStart_PL1.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;
            GridStart_PL2.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;

        }

        PLC1.ResetAttack();
        PLC2.ResetAttack();

        if (DecreaseMap == true)
        {
            PLC1.gameObject.transform.position = new Vector2(0, 0);
            //PLC1.attack.TilesInRange.Clear();
            PLC2.gameObject.transform.position = new Vector2(100, 0);
           // PLC2.attack.TilesInRange.Clear();

            if ( MinSizereached == false)
            {
                GridStart_PL1.position = new Vector2(GridStart_PL1.position.x + 1, GridStart_PL1.position.y - 1);
                GridStart_PL2.position = new Vector2(GridStart_PL2.position.x - 1, GridStart_PL2.position.y - 1);

                Width -= 1;
                Height -= 1;

                GenerateGride();

                if (Width == MinWidth &&
                    Height == MinHeight)
                {
                    MinSizereached = true;
                }
            }
        }
            

    }


    public void DeclareWinner()
    {
        if (Score_PL1 > Score_PL2)
        {

        }

        else if (Score_PL2 > Score_PL1)
        {

        }
    }
}
