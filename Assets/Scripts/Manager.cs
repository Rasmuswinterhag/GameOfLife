using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    [SerializeField] Vector2Int GameSize = new(3,3);
    Cell[][] grid;
    float aliveChance = 0.2f;
    [SerializeField] SpriteRenderer spriteRendererPrefab;

    void Start()
    {
        Application.targetFrameRate = 1;

        grid = new Cell[GameSize.x][];

        for (int x = 0; x < GameSize.x; x++)
        {
            grid[x] = new Cell[GameSize.y];

            for (int y = 0; y < GameSize.y; y++)
            {
                var sr = Instantiate(spriteRendererPrefab, new Vector2(x, y), Quaternion.identity);

                grid[x][y] = new Cell(sr);
                grid[x][y].alive = aliveChance > Random.Range(0f,1f);
                grid[x][y].willBeAlive = grid[x][y].alive;
                grid[x][y].position = new Vector2(x, y);
                grid[x][y].UpdateTetxure();
                
                Debug.Log(grid[x][y].position);
            }
        }
    }

    void Update()
    {
        for (int x = 0; x < GameSize.x; x++)
        {
            for (int y = 0; y < GameSize.y; y++)
            {
                int aliveNeighbors = AliveNeighbors(x, y);

                if(aliveNeighbors < 2) { grid[x][y].willBeAlive = false; }
                if(aliveNeighbors > 3) { grid[x][y].willBeAlive = false; }
                if(aliveNeighbors == 3) { grid[x][y].willBeAlive = true; }

                grid[x][y].UpdateTetxure();
            }
        }
        UpdateCellState();
    }

    int AliveNeighbors(int posX, int posY)
    {
        int aliveNeighbors = 0;

        for (int i = -1; i <= 1; i++)
        {
            if (posX-1 < 0 || posX+1 > GameSize.x-1 || posY-1 < 0 || posY+1 > GameSize.y-1 ) { continue; }

            if(grid[posX-1][posY+i].alive) { aliveNeighbors++; } //Checks first column
            if(i != 0 && grid[posX][posY+i].alive) { aliveNeighbors++; } //Checks second column (excluding current pos)
            if(grid[posX+1][posY+i].alive) { aliveNeighbors++; } //Checks third column
        }
        return aliveNeighbors;
    }

    void UpdateCellState()
    {
        for (int x = 0; x < GameSize.x; x++)
        {
            for (int y = 0; y < GameSize.y; y++)
            {
                grid[x][y].SetNewState();
            }
        }
    }
}