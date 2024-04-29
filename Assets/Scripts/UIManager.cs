using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private Canvas lvlSelectionMenu;

    public void disableLevelSelect()
    {
        lvlSelectionMenu.enabled = false;
    }

    public void SelectLevel()
    {
        lvlSelectionMenu.enabled = true;
        
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Gameplay");

    }
    public void ResumeGame()
    {
        GM.pauseGame();


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
