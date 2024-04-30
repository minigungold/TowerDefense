using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private Canvas lvlSelectionMenu;
    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    private Level level;
    public int lvlID;

    private void Start()
    {

        foreach (GameObject lvlButton in GameObject.FindGameObjectsWithTag("lvlButton"))
        {
            buttonList.Add(lvlButton);
        }

    }
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
