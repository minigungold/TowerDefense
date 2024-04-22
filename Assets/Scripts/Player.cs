using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] public TMP_Text hpText;
    [SerializeField] public TextMeshProUGUI enemyCountText;
    public int waves;
    public int enemy;
    public int enemiesLeft;
    public int maxEnemyCount;
    public static int bonusHP = 0;

    private void Start()
    {
        maxEnemyCount = waves * enemy;
        enemy = maxEnemyCount;
        enemiesLeft = maxEnemyCount;
        enemyCountText.text = $"{enemiesLeft} / {maxEnemyCount}";
    }
    private void Update()
    {
        hpText.text = $"{3 + bonusHP}";
        maxEnemyCount = waves * enemy;
        
    }
}
