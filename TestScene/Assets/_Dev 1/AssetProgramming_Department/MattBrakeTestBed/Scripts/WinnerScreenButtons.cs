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
        AudioManager.Manager.PlayMusic("Credits"); 
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu Animated"); 
    }
}
