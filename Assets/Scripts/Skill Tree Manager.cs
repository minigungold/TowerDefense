using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] private GameObject skillTreeFrame;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void ActivateSkillTree()
    {
        if (skillTreeFrame.activeSelf == true)
        {
            skillTreeFrame.SetActive(false);
        }
        else
        {
            skillTreeFrame.SetActive(true);

        }

    }

}
