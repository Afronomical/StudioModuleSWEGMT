using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{

    public Canvas deathScreen;
    //public Canvas mainCanvas;
    

    /*private void Update()
    {
        if (deathScreen != null)
            AudioManager.Manager.PlayMusic("GameOver");

    }*/

    

    public void Play()
   {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("GameOver");
        AudioManager.Manager.PlayMusic("LevelMusic");
        AudioManager.Manager.StopMusic("BossMusic");

        //GetComponent<Canvas>().enabled = false;
        
        FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Spawn");

        Time.timeScale = 1.0f;


        PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
        //PlayerController.Instance.GetPlayerDeath().SetIsDead(false);
        foreach (BoxCollider2D boxCollider in PlayerController.Instance.GetPlayerDeath().boxColliders)
        {
            if (boxCollider.isTrigger)
                boxCollider.enabled = true;
        }
        PlayerController.Instance.GetPlayerDeath().healthBarScript.SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);

        PlayerController.Instance.GetFeeding().currentHunger = 0;
        CanvasManager.Instance.hungerBarUI.GetComponent<HungerBar>().SetMinHunger(0);
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().setMaxHealth(100);
        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().UpdateHealthBarColour(); 

        CanvasManager.Instance.countdownTimer.timeRemaining = CanvasManager.Instance.countdownTimer.time;
        Transform rotatingCover = CanvasManager.Instance.countdownTimer.rotatingCover;
        rotatingCover.transform.rotation = Quaternion.Euler(rotatingCover.transform.rotation.x, rotatingCover.transform.rotation.y, 0);
        //deathScreen.enabled = false;
        
        //SceneManager.LoadScene("Level 1");
        //SetSpawnpoint.instance.ResetPosition();
        
   }

    public void Menu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("GameOver");
        AudioManager.Manager.StopMusic("BossMusic");
        FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
        //SceneManager.LoadScene("Main Menu Animated");
        deathScreen.enabled = false;
        GameManager.Instance.ChangeGameState(GameManager.GameStates.MainMenu);
        //mainCanvas.gameObject.SetActive(false);
    }

    public void Stats()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        ///load stats screen 
    }
}
