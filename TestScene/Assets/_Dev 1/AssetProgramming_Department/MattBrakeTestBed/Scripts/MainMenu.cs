using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Manager.PlayMusic("MenuMusic");
    }


    public void PlayGame()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("MenuMusic");
        Debug.Log("Play Game");
        SceneManager.LoadScene(1);
        Time.timeScale = 1; 
        //LevelManager.Instance.LoadScene("TestCaveScene");
    }

    public void SettingsMenu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("Settings");
        

    }

    public void InstructionsMenu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("Instructions");
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
