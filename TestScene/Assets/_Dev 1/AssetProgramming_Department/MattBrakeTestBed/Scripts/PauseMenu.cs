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
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
        AudioManager.Manager.StopAudio("LevelMusic");
        
       // loadingScreen.enabled = true;
        
        
      // LevelManager.Instance.LoadScene("MainMenu");
        
    }

    public void Quit()
    {

    }
    public void LoadSettingsMenu()
    {
        Debug.Log("settings menu opened");
    }
}

