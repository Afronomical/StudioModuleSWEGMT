using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
   // public Canvas loadingScreen;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Manager.PlaySFX("UI_Click");
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Main Menu Animated");
        AudioManager.Manager.StopMusic("LevelMusic");
        
       // loadingScreen.enabled = true;
        
        
      // LevelManager.Instance.LoadScene("MainMenu");
        
    }

    public void Quit()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
    }
    public void LoadSettingsMenu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("settings menu opened");
    }
}

