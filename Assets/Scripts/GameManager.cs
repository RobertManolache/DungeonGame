using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ControlsScreen controlsScreen;
    MainMenu mainMenu;

    void Awake()
    {
        
        controlsScreen = FindObjectOfType<ControlsScreen>();
        mainMenu = FindObjectOfType<MainMenu>();
    }
   
    public void LoadControlScreen()
    {
        mainMenu.EnableCanvas();
        controlsScreen.EnableCanvas();        
      
    }
    public void LoadMainMenu()
    {
        mainMenu.EnableCanvas();
        controlsScreen.EnableCanvas();

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
