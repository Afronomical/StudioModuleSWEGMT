using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Manager.PlayMusic("MenuMusic");
        if(CanvasManager.Instance != null)
        {
            CanvasManager.Instance.countdownTimer.gameObject.SetActive(false);
            CanvasManager.Instance.hungerBarUI.gameObject.SetActive(false);
            CanvasManager.Instance.HealthBar.gameObject.SetActive(false);
            CanvasManager.Instance.staminaBar.gameObject.SetActive(false);
        }

        //GameManager.Instance.currentGameState = GameManager.GameStates.MainMenu; 
    }


    public void PlayGame()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("MenuMusic");
        Debug.Log("Play Game");
        SceneManager.LoadScene("Spawn");
        Time.timeScale = 1;
        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.countdownTimer.gameObject.SetActive(true);
            CanvasManager.Instance.hungerBarUI.gameObject.SetActive(true);
            CanvasManager.Instance.HealthBar.gameObject.SetActive(true);
            CanvasManager.Instance.staminaBar.gameObject.SetActive(true);
        }

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
