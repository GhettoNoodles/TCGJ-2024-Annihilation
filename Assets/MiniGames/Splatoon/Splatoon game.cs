using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Splatoongame : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private int gridSizeX;
    [SerializeField] private float cellSize;
    [SerializeField] private int gridSizeY;
    // Start is called before the first frame update
    void Start()
    {
      GenerateGrid();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                var pos = new Vector2(x, y);
                pos *= cellSize/2;
                Instantiate(cellPrefab, pos, quaternion.identity);
            }
        }
    }
}

