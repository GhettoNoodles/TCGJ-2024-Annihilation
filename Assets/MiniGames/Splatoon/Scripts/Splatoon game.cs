using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Splatoongame : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject ediblePrefab;

    [SerializeField] private GameObject holdPanel;
    [SerializeField] private int gridSizeX;
    [SerializeField] private float cellSize;
    [SerializeField] private int gridSizeY;
    [SerializeField] private Image imgP1;
    
    [SerializeField] private Image imgP2;
    [SerializeField] private Color p1;
    [SerializeField] private Color p2;
    private float GameTime = 60;
    private float p1Score = 0;
    private float p2Score = 0;
    private bool gameOver = false;

    private List<Splatoon_Tile> cells;[SerializeField] private float minGameTime;

    // Start is called before the first frame update
    void Start()
    {
        minGameTime += SceneBehaviour.Instance.GameTime;
        SceneBehaviour.Instance.currentGameTime = minGameTime;
        SceneBehaviour.Instance.GameLoaded();
        cells = new List<Splatoon_Tile>();
        GenerateGrid();
        StartCoroutine(TimerRoutine());
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeSinceLevelLoad >= SceneBehaviour.Instance.GameTime)
        {
            Input_Manager.PlayerNumber winner;
            if (p1Score > p2Score)
            {
                winner = Input_Manager.PlayerNumber.P1;
            }
            else if (p1Score < p2Score)
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
    }

    public void UpdateScore()
    {
        float p1Count = 0;
        float p2Count = 0;
        foreach (var cell in cells)
        {
            if (cell.currentDominance == Splatoon_Tile.Dominance.p1)
            {
                p1Count++;
            }
            else if (cell.currentDominance == Splatoon_Tile.Dominance.p2)
            {
                p2Count++;
            }
        }

        p1Score = p1Count / cells.Count;
        p2Score = p2Count / cells.Count;
        imgP1.fillAmount = p1Score;
        imgP2.fillAmount = p2Score;
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                var pos = new Vector2(x, y);
                pos *= cellSize;
                var cell =
                    Instantiate(cellPrefab, pos, quaternion.identity);
                cell.transform.localScale *= cellSize;
                cells.Add(cell.GetComponent<Splatoon_Tile>());
            }
        }
    }

    IEnumerator TimerRoutine()
    {
        while (!gameOver)
        {
            SpawnEdible();
            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnEdible()
    {
        var rand = Random.Range(0, cells.Count);
        Instantiate(ediblePrefab, cells[rand].gameObject.transform.position, quaternion.identity);
    }
}


