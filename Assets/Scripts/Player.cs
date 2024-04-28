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
    public int waves;
    public int enemy;
    public int enemiesLeft;
    public int maxEnemyCount;
    public int gold;
    public static int bonusHP = 0;
    public GameManager gm;

    private void Start()
    {
        Enemy.onDeath += RemoveEnemy;
        //enemy = maxEnemyCount;

    }

    private void RemoveEnemy()
    {
        enemiesLeft--;
        gold++;

    }

    private void Update()
    {
        if (gm.waveOver == false)
        {
            if (gm.waves.Count > 0)
            {
                waves = gm.waves[gm.waveIndex].roundsOfEnemies;
                enemy = gm.waves[gm.waveIndex].enemiesPerRound;
            }
            if (enemiesLeft == 0)
            {
                enemiesLeft = maxEnemyCount;
            }
        }


        hpText.text = $"{3 + bonusHP}";
        goldText.text = $"{gold}";
        maxEnemyCount = waves * enemy;
        enemyCountText.text = $"{enemiesLeft} / {maxEnemyCount}";

    }
}
