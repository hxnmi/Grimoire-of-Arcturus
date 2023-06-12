using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] GameObject mainMenu;
    // public void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.M))
    //     {
    //         mainMenu.SetActive(true);
    //     }
    // }
    public void Play()
    {
        SceneManager.LoadScene("GoA");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
