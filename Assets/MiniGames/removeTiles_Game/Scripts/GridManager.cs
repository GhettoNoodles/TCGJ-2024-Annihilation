using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

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
    [SerializeField] private GameObject holdPanel;
    [SerializeField] private float minGameTime;
    
    // Start is called before the first frame update
    void Start()
    {
        minGameTime += SceneBehaviour.Instance.GameTime;
        SceneBehaviour.Instance.currentGameTime = minGameTime;
        SceneBehaviour.Instance.GameLoaded();

        SpawnCountdown = SpawnReset;
        TextScore1.text = Score_PL1.ToString();
        TextScore2.text = Score_PL2.ToString();
    }

    private void Awake()
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
        if (Time.timeSinceLevelLoad >= SceneBehaviour.Instance.currentGameTime)
        {
            Input_Manager.PlayerNumber winner;
            if (Score_PL1 > Score_PL2)
            {
                winner = Input_Manager.PlayerNumber.P1;
            }
            else if (Score_PL1 < Score_PL2)
            {
                winner = Input_Manager.PlayerNumber.P2;
            }
            else
            {
                winner = (Input_Manager.PlayerNumber)Random.Range(0, 2);
            }

            SceneBehaviour.Instance.EndGameSession(winner);
        }

        if (!(Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
              Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)))
        {
            holdPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            holdPanel.SetActive(false);
            Time.timeScale = 1f;
        }
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

        if (DecreaseMap == true)
        {

            if (MinSizereached == false)
            {

                Width -= 1;
                Height -= 1;



                if (Width == MinWidth &&
                    Height == MinHeight)
                {
                    MinSizereached = true;
                }
            }
        }

        NormalGrid_PL1.Clear();
        NormalGrid_PL2.Clear();
        ResetGrid_PL1();
        ResetGrid_PL2();
     

        PLC1.ResetAttack();
        PLC2.ResetAttack();

        //for (int i = 0; i < GridStart_PL1.childCount; i++)
        //{
        //    Debug.Log("Hello");
        //    //GridStart_PL1.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;
        //    Destroy(GridStart_PL1.GetChild(0).gameObject);
        //    //GridStart_PL2.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;
        //    Destroy(GridStart_PL2.GetChild(0).gameObject);
        //}

       
        //SetPlayerPos();

        
            

    }

    private void ResetGrid_PL2()
    {
        for (int i = 0; i < GridStart_PL2.childCount; i++)
        {
            GameObject ResetTile = GridStart_PL2.GetChild(i).gameObject;
            if (ResetTile.GetComponent<Tile>().tileState != Tile.TileState.Destroyed)
            {
                ResetTile.GetComponent<Tile>().tileState = Tile.TileState.Normal;
                ResetTile.GetComponent<Tile>().SetSprite();
                NormalGrid_PL2.Add(ResetTile);
            }

        }
    }

    private void ResetGrid_PL1()
    {
        for (int i = 0; i < GridStart_PL1.childCount; i++)
        {
           GameObject ResetTile = GridStart_PL1.GetChild(i).gameObject;
           if (ResetTile.GetComponent<Tile>().tileState != Tile.TileState.Destroyed)
            {
                ResetTile.GetComponent<Tile>().tileState = Tile.TileState.Normal;
                ResetTile.GetComponent<Tile>().SetSprite();
                NormalGrid_PL1.Add(ResetTile);
            }

        }
    }

    private void DestroyGrid()
    {
        for (int i = 0; i < GridStart_PL1.childCount; i++)
        {
            Debug.Log("Hello");
            //GridStart_PL1.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;
            Destroy(GridStart_PL1.GetChild(0).gameObject);
            //GridStart_PL2.GetChild(i).gameObject.GetComponent<Tile>().tileState = Tile.TileState.Normal;
            Destroy(GridStart_PL2.GetChild(0).gameObject);
        }
    }

    public void SetPlayer1_Pos()
    {

        int num1 = UnityEngine.Random.Range(0, NormalGrid_PL1.Count);
        PLC1.gameObject.transform.position = NormalGrid_PL1[num1].transform.position;
        PLC1.ScoreOnce = true;
        
        // PLC1.gameObject.transform.position = new Vector2(GridStart_PL1.position.x + 4, GridStart_PL1.position.y + 4);
        // PLC2.gameObject.transform.position = new Vector2(GridStart_PL1.position.x - 4, GridStart_PL1.position.y + 4);
    }

    public void SetPlayer2_Pos()
    {
        int num2 = UnityEngine.Random.Range(0, NormalGrid_PL2.Count);
        PLC2.gameObject.transform.position = NormalGrid_PL2[num2].transform.position;
        PLC2.ScoreOnce = true;
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
