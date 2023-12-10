using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScreenButtons : MonoBehaviour
{

    public Canvas winScreen;
    
    public void EndGame()
    {
        PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
        PlayerController.Instance.GetPlayerDeath().healthBarScript.SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().UpdateHealthBarColour();
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().setMaxHealth(100);
        PlayerController.Instance.GetFeeding().currentHunger = 0;
        CanvasManager.Instance.hungerBarUI.GetComponent<HungerBar>().SetMinHunger(0);

        //FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Credits");
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
        AudioManager.Manager.StopMusic("BossMusic");
        AudioManager.Manager.PlayMusic("Credits");
    }

    public void Menu()
    {
        PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
        PlayerController.Instance.GetPlayerDeath().healthBarScript.SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().UpdateHealthBarColour();
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().setMaxHealth(100);
        PlayerController.Instance.GetFeeding().currentHunger = 0;
        CanvasManager.Instance.hungerBarUI.GetComponent<HungerBar>().SetMinHunger(0);

        AudioManager.Manager.StopMusic("BossMusic");
        //FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
        SceneManager.LoadScene("Main Menu Animated"); 
    }
}
