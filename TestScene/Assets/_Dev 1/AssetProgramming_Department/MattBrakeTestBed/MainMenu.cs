using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    
    
    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene(2);
    }

    public void SettingsMenu()
    {
        Debug.Log("Settings");
        

    }

    public void InstructionsMenu()
    {
        Debug.Log("Instructions");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
