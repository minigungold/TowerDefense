using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TMP_Text hpText;
    [SerializeField] public TextMeshProUGUI enemyCountText;
    [SerializeField] GameObject gameTilePrefab;
    [SerializeField] GameObject enemyPrefab;
    GameTile[,] gameTiles;
    public GameTile spawnTile;
    const int ColCount = 20;
    const int RowCount = 10;
    public static int bonusHP = 0;
    public Player player;
    public int level;
    public List<GameTile> walls;

    public GameTile TargetTile { get; internal set; }
    public List<GameTile> pathToGoal = new List<GameTile>();

    public int waveIndex = 0;
    public bool waveOver = true;
    [SerializeField] private int nbOfWaves = 1;
    [SerializeField] public List<Wave> waves = new List<Wave>();

    private void Awake()
    {
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

        //walls.Add(gameTiles[5, 9]);
        //walls.Add(gameTiles[5, 8]);
        //walls.Add(gameTiles[5, 7]);

        //foreach (GameTile tile in walls)
        //{
        //    tile.wallRenderer.enabled = true;
        //}
        PlaceWalls();
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space) && TargetTile != null)
        {
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

            StartCoroutine(SpawnEnemyCoroutineTest());
            //if (enemyPrefab.IsDestroyed())
            //{
            //    enemyCountText.text = $"{player.enemiesLeft} / {maxEnemyCount}";
            //}
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("ArbreCompetences");
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
    private void createLevel()
    {
        switch (level)
        {
            case 0:
                walls.Add(gameTiles[5, 9]);
                walls.Add(gameTiles[5, 8]);
                walls.Add(gameTiles[5, 7]);
                walls.Add(gameTiles[5, 6]);
                walls.Add(gameTiles[5, 5]);

                walls.Add(gameTiles[9, 5]);
                walls.Add(gameTiles[9, 4]);
                walls.Add(gameTiles[9, 3]);
                walls.Add(gameTiles[9, 2]);
                walls.Add(gameTiles[9, 1]);
                walls.Add(gameTiles[9, 0]);
                break;

            case 1:
                walls.Add(gameTiles[5, 9]);
                walls.Add(gameTiles[5, 8]);
                walls.Add(gameTiles[5, 7]);
                walls.Add(gameTiles[5, 6]);
                walls.Add(gameTiles[5, 5]);

                walls.Add(gameTiles[9, 5]);
                walls.Add(gameTiles[9, 4]);
                walls.Add(gameTiles[9, 3]);
                walls.Add(gameTiles[9, 2]);
                walls.Add(gameTiles[9, 1]);
                walls.Add(gameTiles[9, 0]);
                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;
        }
    }

    IEnumerator SpawnEnemyCoroutineTest()
    {
        waveOver = false;
        for (waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            Wave wave = waves[waveIndex];
            for (int j = 0; j < wave.roundsOfEnemies; j++)
            {

                for (int i = 0; i < wave.enemiesPerRound; i++)
                {
                    yield return new WaitForSeconds(wave.enemiesDelay);
                    var enemy = Instantiate(enemyPrefab, spawnTile.transform.position, Quaternion.identity);
                    enemy.GetComponent<Enemy>().SetPath(pathToGoal);

                }
                yield return new WaitForSeconds(wave.roundDelay);
            }

        }
        waveOver = true;
    }
}
