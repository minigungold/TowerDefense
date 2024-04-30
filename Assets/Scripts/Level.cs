using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private int objectLvl;
    [SerializeField] private TextMeshProUGUI levelText;
    public static int level;
    private Button button;


    private void Start()
    {
        button = GetComponent<Button>();
    }
    private void Update()
    {
        
    }

    public void SelectLevel()
    {
        level = objectLvl;
        //GM.level = this.level;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //button.onClick.Invoke();
        //SceneManager.LoadScene($"Level{this.level}");
    }

}
