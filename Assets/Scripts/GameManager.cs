using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    [SerializeField] private Canvas menuLvlSelect;
    [SerializeField] private Button resumeButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TextMeshProUGUI levelText;
    public TMP_Text hpText;
    public TextMeshProUGUI enemyCountText;
    [SerializeField] GameObject gameTilePrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] GameObject enemyPrefab3;
    GameTile[,] gameTiles;
    public GameTile spawnTile;
    const int ColCount = 20;
    const int RowCount = 10;
    public int bonusHP = 0;
    public Player player;
    public int level;
    public List<GameTile> walls;

    public GameTile TargetTile { get; internal set; }
    private List<GameTile> pathToGoal = new List<GameTile>();

    public int waveIndex = 0;
    public bool waveOver = true;
    [SerializeField] private int nbOfWaves = 1;
    public List<Wave> waves = new List<Wave>();

    public bool gameStarted = false;
    private bool isPaused = false;
    private bool lvlCreated = false;
    public bool roundOver = true;

    private void Awake()
    {
        level = Level.level;
        
        Time.timeScale = 1.0f;
        gameTiles = new GameTile[ColCount, RowCount];

        for (int x = 0; x < ColCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                var spawnPosition = new Vector3(x, y, 0);

                var tile = Instantiate(gameTilePrefab, spawnPosition, Quaternion.identity); // .identity = rotation de 0
                gameTiles[x, y] = tile.GetComponent<GameTile>();
                gameTiles[x, y].GM = this;
                gameTiles[x, y].X = x;
                gameTiles[x, y].Y = y;

                if ((x + y) % 2 == 0)
                {
                    gameTiles[x, y].TurnGray();
                }

            }
        }

        spawnTile = gameTiles[1, 7];
        spawnTile.SetEnemySpawn();
        levelText.text = $"Level : {level + 1}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
        if (gameStarted == false)
        {
            createLevel();

            foreach (var t in gameTiles)
            {
                t.SetPath(false);
            }

            var path = Pathfinding(spawnTile, TargetTile);
            var tile = TargetTile;

            while (tile != null)
            {
                pathToGoal.Add(tile); //ajoute la tuile dans une pile de destinations
                tile.SetPath(true);
                tile = path[tile];
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                StartCoroutine(SpawnEnemyCoroutineTest());
                gameStarted = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("ArbreCompetences");
        }

        if (waveOver == true && player.enemiesLeft == 0)
        {
            isPaused = true;
            menu.enabled = true;
            resumeButton.interactable = false;
            victoryText.enabled = true;
            gameStarted = false;
            waveIndex = 0;
            Time.timeScale = 0f;
        }

        if (player.gameOver == true)
        {
            isPaused = true;
            menu.enabled = true;
            resumeButton.interactable = false;
            gameOverText.enabled = true;
            gameStarted = false;
            waveIndex = 0;
            Time.timeScale = 0f;
        }

    }
    private Dictionary<GameTile, GameTile> Pathfinding(GameTile sourceTile, GameTile targetTile)
    {
        // distance minimale de la tuile à la source
        var dist = new Dictionary<GameTile, int>();

        // tuile précédente qui mène au chemin le plus court
        var prev = new Dictionary<GameTile, GameTile>();

        // liste des tuiles restantes
        var Q = new List<GameTile>();

        foreach (var v in gameTiles)
        {
            // dist[v] <- INFINITY
            dist.Add(v, 9999);

            // prev <- UNDEFINED
            prev.Add(v, null);

            //add v to Q
            Q.Add(v);
        }
        // dist[source] <- 0
        dist[sourceTile] = 0;

        while (Q.Count > 0)
        {
            GameTile u = null;
            int minDistance = int.MaxValue;

            foreach (var v in Q)
            {
                if (dist[v] < minDistance)
                {
                    minDistance = dist[v];
                    u = v;
                }
            }


            Q.Remove(u);

            foreach (var v in FindNeighbor(u))
            {
                if (!Q.Contains(v) || v.IsBlocked)
                {
                    continue;

                }

                int alt = dist[u] + 1;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }

            }

        }
        return prev;
    }

    private List<GameTile> FindNeighbor(GameTile u)
    {
        var result = new List<GameTile>();

        if (u.X - 1 >= 0)
            result.Add(gameTiles[u.X - 1, u.Y]);
        if (u.X + 1 < ColCount)
            result.Add(gameTiles[u.X + 1, u.Y]);
        if (u.Y - 1 >= 0)
            result.Add(gameTiles[u.X, u.Y - 1]);
        if (u.Y + 1 < RowCount)
            result.Add(gameTiles[u.X, u.Y + 1]);
        return result;
    }
    private void PlaceWalls()
    {
        foreach (GameTile tile in walls)
        {
            tile.wallRenderer.enabled = true;
        }
    }

    private void RemoveWalls()
    {
        foreach (GameTile tile in walls)
        {
            tile.wallRenderer.enabled = true;
        }
    }
    private void createLevel()
    {
        switch (level)
        {
            case 0:
                if (lvlCreated == false)
                {

                    for (int x = 0; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 9]);
                        walls.Add(gameTiles[x, 0]);
                    }
                    for (int y = 0; y <= 9; y++)
                    {
                        walls.Add(gameTiles[0, y]);
                        walls.Add(gameTiles[19, y]);
                    }
                    for (int y = 0; y <= 6; y++)
                    {
                        walls.Add(gameTiles[5, y]);
                    }

                    walls.Add(gameTiles[5, 9]);
                    walls.Add(gameTiles[5, 8]);

                    for (int y = 0; y <= 4; y++)
                    {
                        walls.Add(gameTiles[9, y]);
                    }
                    for (int y = 9; y >= 6; y--)
                    {
                        walls.Add(gameTiles[9, y]);
                    }

                    for (int y = 0; y <= 1; y++)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    for (int y = 9; y >= 3; y--)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    lvlCreated = true;
                }
                TargetTile = gameTiles[18, 6];



                break;

            case 1:
                if (lvlCreated == false)
                {
                    for (int x = 0; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 9]);
                        walls.Add(gameTiles[x, 0]);
                    }
                    for (int y = 0; y <= 9; y++)
                    {
                        walls.Add(gameTiles[0, y]);
                        walls.Add(gameTiles[19, y]);
                    }


                    for (int y = 0; y <= 7; y++)
                    {
                        walls.Add(gameTiles[4, y]);
                        walls.Add(gameTiles[12, y]);
                    }
                    //for (int y = 3; y >= 7; y--)
                    //{
                    //    walls.Add(gameTiles[4, y]);
                    //    walls.Add(gameTiles[19, y]);
                    //}

                    for (int y = 2; y <= 9; y++)
                    {
                        walls.Add(gameTiles[8, y]);
                    }

                }

                TargetTile = gameTiles[17, 1];
                break;

            case 2:
                if (lvlCreated == false)
                {

                    for (int x = 0; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 9]);
                        walls.Add(gameTiles[x, 0]);
                    }
                    for (int y = 0; y <= 9; y++)
                    {
                        walls.Add(gameTiles[0, y]);
                        walls.Add(gameTiles[19, y]);
                    }
                    for (int y = 5; y <= 9; y++)
                    {
                        walls.Add(gameTiles[5, y]);
                    }

                    walls.Add(gameTiles[5, 1]);
                    walls.Add(gameTiles[5, 2]);

                    for (int y = 0; y <= 5; y++)
                    {
                        walls.Add(gameTiles[9, y]);
                    }
                    for (int y = 9; y >= 7; y--)
                    {
                        walls.Add(gameTiles[9, y]);
                    }

                    for (int y = 0; y <= 1; y++)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    for (int y = 9; y >= 3; y--)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    lvlCreated = true;
                }
                TargetTile = gameTiles[18, 6];
                break;

            case 3:
                if (lvlCreated == false)
                {

                    for (int x = 0; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 9]);
                        walls.Add(gameTiles[x, 0]);
                    }
                    for (int y = 0; y <= 9; y++)
                    {
                        walls.Add(gameTiles[0, y]);
                        walls.Add(gameTiles[19, y]);
                    }
                    for (int y = 5; y <= 9; y++)
                    {
                        walls.Add(gameTiles[5, y]);
                    }

                    walls.Add(gameTiles[5, 1]);
                    walls.Add(gameTiles[5, 2]);

                    for (int y = 0; y <= 5; y++)
                    {
                        walls.Add(gameTiles[9, y]);
                    }
                    for (int y = 9; y >= 7; y--)
                    {
                        walls.Add(gameTiles[9, y]);
                    }

                    for (int y = 0; y <= 7; y++)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    for (int y = 9; y >= 9; y--)
                    {
                        walls.Add(gameTiles[13, y]);
                    }
                    walls.Add(gameTiles[14, 7]);
                    walls.Add(gameTiles[15, 7]);
                    lvlCreated = true;
                }
                TargetTile = gameTiles[16, 2];
                break;

            case 4:
                if (lvlCreated == false)
                {
                    for (int x = 0; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 9]);
                        walls.Add(gameTiles[x, 0]);
                    }
                    for (int y = 0; y <= 9; y++)
                    {
                        walls.Add(gameTiles[0, y]);
                        walls.Add(gameTiles[19, y]);
                    }

                    for (int x = 0; x <= 16; x++)
                    {
                        walls.Add(gameTiles[x, 6]);
                        //walls.Add(gameTiles[x, 0]);
                    }
                    for (int x = 3; x <= 19; x++)
                    {
                        walls.Add(gameTiles[x, 3]);
                        //walls.Add(gameTiles[x, 0]);
                    }

                }
                TargetTile = gameTiles[17, 1];
                break;
        }
        PlaceWalls();
    }

    IEnumerator SpawnEnemyCoroutineTest()
    {
        waveOver = false;
        for (waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            roundOver = false;
            Wave wave = waves[waveIndex];

            player.maxEnemyCount = (wave.roundsOfEnemies * wave.enemiesPerRound) + 3 *wave.roundsOfEnemies;
            player.enemiesLeft = (wave.roundsOfEnemies * wave.enemiesPerRound) + (3 * wave.roundsOfEnemies);

            for (int j = 0; j < wave.roundsOfEnemies; j++)
            {
                for (int i = 0; i < wave.enemiesPerRound; i++)
                {
                    yield return new WaitForSeconds(wave.enemiesDelay);
                    var enemy = Instantiate(enemyPrefab, spawnTile.transform.position, Quaternion.identity);
                    enemy.GetComponent<Enemy>().SetPath(pathToGoal);

                }
                yield return new WaitForSeconds(wave.enemiesDelay);

                var enemy2 = Instantiate(enemyPrefab2, spawnTile.transform.position, Quaternion.identity);
                enemy2.GetComponent<Enemy>().SetPath(pathToGoal);

                for (int i = 0; i < 2; i++)
                {
                    yield return new WaitForSeconds(wave.enemiesDelay);
                    var enemy3 = Instantiate(enemyPrefab3, spawnTile.transform.position, Quaternion.identity);
                    enemy3.GetComponent<Enemy>().SetPath(pathToGoal);
                }
                yield return new WaitForSeconds(wave.roundDelay);
            }
            while (player.enemiesLeft != 0)
            {
                yield return null;
            }
            roundOver = true;

        }
        waveOver = true;

    }

    public void pauseGame()
    {
        uiManager.disableLevelSelect();
        //menu.enabled = !menu.enabled;
        if (!menu.enabled)
        {
            isPaused = true;
            menu.enabled = true;
            Time.timeScale = 0;
        }
        else if (menu.enabled)
        {
            isPaused = false;
            menu.enabled = false;
            Time.timeScale = 1;
        }

    }


}