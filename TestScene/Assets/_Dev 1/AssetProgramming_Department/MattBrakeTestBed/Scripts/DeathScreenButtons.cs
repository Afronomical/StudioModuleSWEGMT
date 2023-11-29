using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{

    public Canvas deathScreen;

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
        GetComponent<Canvas>().enabled = false;
        PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
        PlayerController.Instance.GetPlayerDeath().SetIsDead(false);
        foreach (BoxCollider2D boxCollider in PlayerController.Instance.GetPlayerDeath().boxColliders)
        {
            if (boxCollider.isTrigger)
                boxCollider.enabled = true;
        }
        PlayerController.Instance.GetPlayerDeath().healthBarScript.SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
        CanvasManager.Instance.countdownTimer.timeRemaining = CanvasManager.Instance.countdownTimer.time;
        Transform rotatingCover = CanvasManager.Instance.countdownTimer.rotatingCover;
        rotatingCover.transform.rotation = Quaternion.Euler(rotatingCover.transform.rotation.x, rotatingCover.transform.rotation.y, 0);
        SceneManager.LoadScene("Level 1");
        SetSpawnpoint.instance.ResetPosition();
   }

    public void Menu()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("GameOver");
        SceneManager.LoadScene("Main Menu Animated");
    }

    public void Stats()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        ///load stats screen 
    }
}
