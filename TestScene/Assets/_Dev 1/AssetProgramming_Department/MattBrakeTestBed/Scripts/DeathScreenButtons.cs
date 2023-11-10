using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{

    public Canvas deathScreen; 
    
    
   public void Play()
   {
        SceneManager.LoadScene(1);
   }

    public void Menu()
    {
        SceneManager.LoadScene(2);
    }

    public void Stats()
    {
        ///load stats screen 
    }
}
