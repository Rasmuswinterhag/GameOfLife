using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Vector2Int GameSize = new(3,3);
    Cell[][] grid;
    [SerializeField] SpriteRenderer spriteRendererPrefab;

    void Start()
    {
        //Application.targetFrameRate = 30;

        grid = new Cell[GameSize.x][];

        for (int x = 0; x < GameSize.x; x++)
        {
            grid[x] = new Cell[GameSize.y];

            for (int y = 0; y < GameSize.y; y++)
            {
                var sr = Instantiate(spriteRendererPrefab, new Vector2(x, y), Quaternion.identity);

                grid[x][y] = new Cell(sr);
                grid[x][y].position = new Vector2(x, y);
                
                Debug.Log(grid[x][y].position);
            }
        }
    }
}
