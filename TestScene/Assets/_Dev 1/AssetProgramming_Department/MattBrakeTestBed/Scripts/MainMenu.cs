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
        SceneManager.LoadScene("Spawn");
        Time.timeScale = 1; 
        //LevelManager.Instance.LoadScene("TestCaveScene");
    }

    public void SettingsMenu()
    {
        //AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("Settings");
        SceneManager.LoadScene("SettingsMenu");
        

    }

    public void TutorialLevel()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("Tutorial");
        AudioManager.Manager.StopMusic("MenuMusic");
       // SceneManager.LoadScene("Tutorial_Level");
        //SceneManager.LoadScene("InstructionsMenu");
    }

    public void Credits()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("MenuMusic");
        SceneManager.LoadScene("Credits"); 
    }

    public void ExitGame()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
