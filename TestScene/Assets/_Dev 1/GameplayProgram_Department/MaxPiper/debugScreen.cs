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
    //private playerAttack playerAttack;
    public GameObject togGodVis;
    public GameObject togInstaVis;
    private int origDam;



    void Start()
    {
        godButton.SetActive(false);
        instaButton.SetActive(false);
        nextLevelButton.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        origDam = player.GetComponent<playerAttack>().damage;
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
            togGodVis.SetActive(false);
        }
        else
        {
            player.GetComponent<PlayerDeath>().godMode = true;
            godPressed = true;
            togGodVis.SetActive(true);
        }
    }

    public void instakill()
    {
        instaPressed = !instaPressed;

        if (instaPressed)
        {
            // Set attack damage to a high value for instant kill
            player.GetComponent<playerAttack>().damage = 9999;
            togInstaVis.SetActive(true);
        }
        else
        {
            // Restore the original attack damage
            player.GetComponent<playerAttack>().damage = origDam;
            togInstaVis.SetActive(false);
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
