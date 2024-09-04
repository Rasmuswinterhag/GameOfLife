using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Vector2Int GameSize = new(3,3);
    Cell[][] grid;

    void Start()
    {
        Application.targetFrameRate = 30;

        grid = new Cell[GameSize.x][];

        for (int x = 0; x < GameSize.x; x++)
        {
            grid[x] = new Cell[GameSize.y];

            for (int y = 0; y < GameSize.y; y++)
            {
                grid[x][y] = new Cell();
            }
        }

        Debug.Log(grid);
    }

}
