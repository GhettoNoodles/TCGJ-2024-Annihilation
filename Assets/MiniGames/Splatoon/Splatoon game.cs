using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Splatoongame : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private int gridSizeX;
    [SerializeField] private float cellSize;
    [SerializeField] private int gridSizeY;
    [SerializeField] private Image imgP1;
    [SerializeField] private TextMeshProUGUI texttimer;
    [SerializeField] private Image imgP2;
    [SerializeField] private Color p1;
    [SerializeField] private Color p2;

    private float p1Score =0 ;
    private float p2Score =0;

    private List<Splatoon_Tile> cells;
    
    // Start is called before the first frame update
    void Start()
    {
        cells = new List<Splatoon_Tile>();
      GenerateGrid();  
    }

    // Update is called once per frame
    void Update()
    {
        texttimer.text = Time.timeSinceLevelLoad.ToString("F0");

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
            else if (cell.currentDominance==Splatoon_Tile.Dominance.p2)
            {
                p2Count++;
            }
        }
        p1Score = p1Count / cells.Count;
        p2Score = p2Count / cells.Count;
        Debug.Log(p1Score);
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
                var cell=
                Instantiate(cellPrefab, pos, quaternion.identity);
                cell.transform.localScale *= cellSize;
                cells.Add(cell.GetComponent<Splatoon_Tile>());
            }
        }
    }
}

