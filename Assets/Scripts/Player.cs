using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    private int maxEnemyCount = 15;
    private int leftEnemiesCount;
    public static int bonusHP = 0;

    private void Start()
    {
        leftEnemiesCount = maxEnemyCount;
    }
    private void Update()
    {
        hpText.text = $"{3 + bonusHP}";
        enemyCountText.text = $"{leftEnemiesCount} / {maxEnemyCount}";
    }
}
