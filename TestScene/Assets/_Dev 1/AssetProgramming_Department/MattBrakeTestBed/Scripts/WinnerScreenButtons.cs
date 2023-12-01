using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScreenButtons : MonoBehaviour
{

    public Canvas winScreen;
    
  public void EndGame()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
        AudioManager.Manager.StopMusic("BossMusic");
        AudioManager.Manager.PlayMusic("Credits"); 
    }

    public void Menu()
    {
        AudioManager.Manager.StopMusic("BossMusic"); 
        SceneManager.LoadScene("Main Menu Animated"); 
    }
}
