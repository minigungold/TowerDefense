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

    private void Start()
    {
        gold = 450;
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
    private void RemoveEnemy()
    {
        enemiesLeft--;
        gold += 50;

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
        if (enemiesLeft == 0)
        {
            enemiesLeft = maxEnemyCount;
        }



        hpText.text = $"{HP + bonusHP}";
        goldText.text = $"{gold}";
        maxEnemyCount = (waves * enemy) + waves;
        enemyCountText.text = $"{enemiesLeft} / {maxEnemyCount}";
        waveCountText.text = $"{gm.waveIndex + 1}";

    }
}
