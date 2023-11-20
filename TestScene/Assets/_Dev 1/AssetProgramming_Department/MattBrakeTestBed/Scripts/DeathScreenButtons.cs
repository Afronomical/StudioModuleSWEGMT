using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{

    public Canvas deathScreen; 
    
    
   public void Play()
   {
        AudioManager.Manager.PlaySFX("UI_Click");
        AudioManager.Manager.StopMusic("GameOver");
        SceneManager.LoadScene("Level 1 ");
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
