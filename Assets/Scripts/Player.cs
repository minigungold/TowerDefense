using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI waveCountText;
    public int waves;
    public int enemy;
    public int enemiesLeft;
    public int maxEnemyCount;
    public float gold;
    public static int bonusHP = 0;
    public int HP = 3;
    public GameManager gm;
    public bool gameOver = false;
    public bool victory = false;

    private void Start()
    {
        bonusHP = 0;
        HP = 3;
        gold = 500;
        Enemy.onDeath += RemoveEnemy;
        GameTile.onAttackPlayer += AttackPlayer;
        //enemy = maxEnemyCount;

    }
    private void GameOver()
    {
        if (HP + bonusHP <= 0)
        {
            gameOver = true;
        }
    }

    private void Victory()
    {
        if (!gameOver && enemiesLeft == 0 && gm.waveIndex == gm.waves.Count - 1)
        {
            victory = true;
        }
    }
    private void RemoveEnemy()
    {
        enemiesLeft--;
        gold += 60;

    }
    private void AttackPlayer()
    {
        HP--;

    }
    private void Update()
    {
        GameOver();

        if (gm.waves.Count > 0)
        {
            waves = gm.waves[gm.waveIndex].roundsOfEnemies;
            enemy = gm.waves[gm.waveIndex].enemiesPerRound;
        }

        if (enemiesLeft == 0 && gm.roundOver)
        {
            enemiesLeft = (waves * enemy) + 3 * waves;
        }

        hpText.text = $"{HP + bonusHP}";
        goldText.text = $"{gold}";
        maxEnemyCount = (waves * enemy) + 3 * waves;
        enemyCountText.text = $"{enemiesLeft} / {maxEnemyCount}";
        waveCountText.text = $"{gm.waveIndex + 1}";







    }
}
