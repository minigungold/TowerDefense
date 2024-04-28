using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
    public void ResumeGame()
    {
        GM.pauseGame();


    }

    public void RestartGame()
    {

    }

    public void ExitGame()
    {

    }

}
