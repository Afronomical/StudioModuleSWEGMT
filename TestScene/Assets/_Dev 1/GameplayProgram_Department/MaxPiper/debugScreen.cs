using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debugMode : MonoBehaviour
{
    public GameObject nextLevelButton;
    public GameObject godButton;
    public GameObject instaButton;
    private bool visiPressed;
    private bool godPressed;
    private bool instaPressed;
    public GameObject player;
    private PlayerDeath playerDeath;
    private playerAttack playerAttack;
    private float origAttackDelay;



    void Start()
    {
        godButton.SetActive(false);
        instaButton.SetActive(false);
        nextLevelButton.SetActive(false);
        origAttackDelay = player.GetComponent<playerAttack>().attackDelayStart;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            
            
        }
    }

    public void godMode()
    {
        if (godPressed)
        {
            player.GetComponent<PlayerDeath>().godMode = false;
            godPressed = false;
        }
        else
        {
            player.GetComponent<PlayerDeath>().godMode = true;
            godPressed = true;
        }
    }

    public void instakill()
    {
        instaPressed = !instaPressed;

        if (instaPressed)
        {
            // Set attack damage to a high value for instant kill
            player.GetComponent<playerAttack>().damage = 1000;
        }
        else
        {
            // Restore the original attack damage
            player.GetComponent<playerAttack>().damage = 1;
        }
    }

    public void LoadNextLevel()
    {
        // Assuming your scenes are organized with sequential names like "Level1", "Level2", etc.
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void menuVisible()
    {
        if (visiPressed)
        {
            godButton.SetActive(false);
            instaButton.SetActive(false);
            nextLevelButton.SetActive(false);
            visiPressed = false;
        }
        else
        {
            godButton.SetActive(true);
            instaButton.SetActive(true);
            nextLevelButton.SetActive(true);
            visiPressed = true;
        }
    }
}
