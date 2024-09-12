using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell
{
    public bool alive = false;
    public bool willBeAlive;
    SpriteRenderer sr;
    public Vector2 position;
    int stableTicks = 0;

    public Cell(SpriteRenderer spriteRenderer, float aliveChance, Vector2 spawnPosition)
    {
        sr = spriteRenderer;
        alive = aliveChance > Random.Range(0f,1f);
        willBeAlive = alive;
        position = spawnPosition;

        UpdateTetxure();
    }

    public void UpdateTetxure()
    {
        if(alive)
        {
            sr.color = Color.Lerp(Color.white, Color.green, stableTicks * 0.1f);
        }
        else
        {
            sr.color = Color.black;
        }
    }

    public void SetNewState(bool countStability)
    {
            if (alive == willBeAlive && countStability)
            {
                stableTicks++;
            }
            else 
            {
                stableTicks = 0;
            }
        alive = willBeAlive;
        UpdateTetxure();
    }
}





public class Game : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] SpriteRenderer spriteRendererPrefab;
    Vector2Int gameSize = new(160,90);
    float tickRate = 0.2f;
    float aliveChance = 0.2f;
    bool pause = false;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI tickrateSliderValueText;
    [SerializeField] Slider tickrateSlider;
    [SerializeField] TextMeshProUGUI pauseButtonText;
    [SerializeField] TextMeshProUGUI aliveChanceSliderValueText;
    [SerializeField] Slider aliveChanceSlider;

    [Header("Hidden Varables")]
    Cell[][] grid;
    float tickTimer = 0;
    Vector2Int lastDrawPos;
    bool drew;

    void Start()
    {
        Application.targetFrameRate = 60;

        gameSize = Messanger.instance.menuGameSize;
        aliveChance = Messanger.instance.menuAliveChance;
        
        aliveChanceSlider.value = aliveChance;
        AliveChanceSlider(aliveChance);

        tickRate = tickrateSlider.value;
        TickSlider(tickRate);

        grid = new Cell[gameSize.x][];
        for (int x = 0; x < gameSize.x; x++)
        {
            grid[x] = new Cell[gameSize.y];

            for (int y = 0; y < gameSize.y; y++)
            {
                var sr = Instantiate(spriteRendererPrefab, new Vector2(x, y), Quaternion.identity, transform);
                grid[x][y] = new Cell(sr, aliveChance, new Vector2(x,y));
            }
        }

        FixCamera();

    }

    void Update()
    {
        Draw();
        if(!pause)
        {
            tickTimer += Time.deltaTime;
            if(tickTimer >= tickRate)
            {
                Tick();
                tickTimer = 0;
            }
        }

    }

    void Tick()
    {   
        for (int x = 0; x < gameSize.x; x++)
        {
            for (int y = 0; y < gameSize.y; y++)
            {
                int aliveNeighbors = AliveNeighbors(x, y);

                if(aliveNeighbors < 2) { grid[x][y].willBeAlive = false; }
                if(aliveNeighbors > 3) { grid[x][y].willBeAlive = false; }
                if(aliveNeighbors == 3) { grid[x][y].willBeAlive = true; }
            }
        }
        UpdateCellState();
    }

    int AliveNeighbors(int posX, int posY)
    {
        int aliveNeighbors = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if(posX+i < 0 || posX+i > gameSize.x-1 || posY+j < 0 || posY+j > gameSize.y-1 || (i == 0 && j == 0)) {continue;}
                if(grid[posX+i][posY+j].alive)
                {
                    aliveNeighbors++;
                }
            }
        }
        return aliveNeighbors;
    }

    void UpdateCellState()
    {
        for (int x = 0; x < gameSize.x; x++)
        {
            for (int y = 0; y < gameSize.y; y++)
            {
                grid[x][y].SetNewState(true);
            }
        }
    }

    void Draw()
    {
        Vector2Int mousePos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(mousePos.x < 0 || mousePos.y < 0 || mousePos.x > gameSize.x-1 || mousePos.y > gameSize.y-1) {return;}
        if(Input.GetMouseButton(0))
        {
            if(Input.GetMouseButtonDown(0)) { drew = !grid[mousePos.x][mousePos.y].alive; }
            if(lastDrawPos != mousePos)
            {
                grid[mousePos.x][mousePos.y].willBeAlive = drew;
                grid[mousePos.x][mousePos.y].SetNewState(false);
                lastDrawPos = mousePos;
            }
        }
        if(Input.GetMouseButtonUp(0)) {lastDrawPos = new(-1, -1);}
    }

    void FixCamera()
    {
        Camera.main.transform.position = new Vector3(gameSize.x/2f, gameSize.y/2f, -10) - new Vector3(0.5f,0.5f);
        Camera.main.orthographicSize = Mathf.Max(gameSize.y/2f, gameSize.x/(2f*Camera.main.aspect));
    }


    #region UI
        public void PauseButton()
        {
            pause = !pause;

            if(pause)
            {
                pauseButtonText.text = "Resume";
            }
            else
            {
                pauseButtonText.text = "Pause";
            }
        }

        public void TickSlider(float value)
        {
            tickrateSliderValueText.text = value.ToString("0.0");

            tickRate = value;
        }

        public void TickButton()
        {
            Tick();
        }

        public void Clear()
        {
            for (int x = 0; x < gameSize.x; x++)
            {
                for (int y = 0; y < gameSize.y; y++)
                {
                    grid[x][y].willBeAlive = false;
                }
            }
            UpdateCellState();
        }

        public void Generate()
        {
            for (int x = 0; x < gameSize.x; x++)
            {
                for (int y = 0; y < gameSize.y; y++)
                {
                    grid[x][y].willBeAlive = aliveChance > Random.Range(0f,1f);
                }
            }
            UpdateCellState();
        }

        public void AliveChanceSlider(float value)  
        {
            aliveChanceSliderValueText.text = (value * 100).ToString("0") + "%";

            aliveChance = value;
        }

    #endregion
}