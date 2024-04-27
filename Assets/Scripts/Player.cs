using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    private void Start()
    {
        Enemy.onDeath += RemoveEnemy;

        maxEnemyCount = waves * enemy;
        //enemy = maxEnemyCount;
        enemiesLeft = maxEnemyCount;
    }

    private void RemoveEnemy()
    {
        enemiesLeft--;
        gold++;

    }

    private void Update()
    {
        hpText.text = $"{3 + bonusHP}";
        goldText.text = $"{gold}";
        maxEnemyCount = waves * enemy;
        enemyCountText.text = $"{enemiesLeft} / {maxEnemyCount}";
        
    }
}
