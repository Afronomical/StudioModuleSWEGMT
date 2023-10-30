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
        AudioManager.Manager.StopAudio("MenuMusic");
        Debug.Log("Play Game");
        SceneManager.LoadScene(2);
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
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
