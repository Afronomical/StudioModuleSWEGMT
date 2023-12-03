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

    private GameObject playerObject;

    private void Start()
    {
        playerObject = FindFirstObjectByType<PlayerController>().gameObject;
        GameManager.Instance.ChangeGameState(GameManager.GameStates.PauseMenu);
    }


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
        AudioManager.Manager.stopAllInGameSFX();

        playerObject.GetComponent<playerAttack>().enabled = false;
        playerObject.GetComponent<PlayerController>().enabled = false;
        playerObject.GetComponent<PlayerAnimationChange>().enabled = false;
    }
    public void Resume()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.ChangeGameState(GameManager.GameStates.PlayerInLevel);
        playerObject.GetComponent<playerAttack>().enabled = true;
        playerObject.GetComponent<PlayerController>().enabled = true;
        playerObject.GetComponent<PlayerAnimationChange>().enabled = true;
    }

    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioManager.Manager.PlaySFX("UI_Click");
        pauseMenuUI.SetActive(false);
        FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
        //SceneManager.LoadScene("Main Menu Animated");
        AudioManager.Manager.StopMusic("LevelMusic");
        AudioManager.Manager.StopMusic("BossMusic");
        //GameManager.Instance.currentGameState = GameManager.GameStates.PlayerDead;
        //GameManager.Instance.ChangeGameState(GameManager.GameStates.MainMenu);
        CanvasManager.Instance.countdownTimer.gameObject.SetActive(false);
        CanvasManager.Instance.hungerBarUI.slider.value = 0;
        CanvasManager.Instance.hungerBarUI.gameObject.SetActive(false);

        // loadingScreen.enabled = true;


        // LevelManager.Instance.LoadScene("MainMenu");

    }

    public void Quit()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Application.Quit();
    }
    public void LoadSettingsMenu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        Debug.Log("settings menu opened");
        AudioManager.Manager.StopMusic("LevelMusic");
        AudioManager.Manager.StopMusic("BossMusic");
    }
}

