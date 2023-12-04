/*
This is the game manager script.
It is responsible for tracking the state the game is currently in
and changing between states according to what is happening to the player.

It can be placed on any object in the scene.

Written by Lucian in the AI team 
 */


//I've edited this script to make it so that you can it static

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool playerIsDead;
    public bool canChangeLevel;

    private GameObject pauseMenu;
    private bool hasPaused;

    //this is where the conditions that trigger state changes are defined
    //they are simple for now, subject to change as per Tech Design requirements
    public int peopleEatingThreshold;
    public int peopleEaten;


    public GameObject player;
    public Transform playerSpawn;
    private CountdownTimer timer;
    public string nextLevel = "Level 2";
    public string currentLevel = "Level 1";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public enum GameStates
    {
        //add whichever states to this
        PlayerDead,
        ObjectiveCompleted,//objective has to be clearly defined
        ObjectiveFailed,
        PlayerInLevel,
        GodMode,
        PauseMenu,
        MainMenu 
       

    }

    public GameStates currentGameState;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(player, playerSpawn);

        //get reference to player
        //player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);

        //reference to game timer
        timer = GameObject.Find("Timer").GetComponent<CountdownTimer>();
        //default state
        currentGameState = GameStates.PlayerInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == null)
        {
            timer = FindFirstObjectByType<CountdownTimer>();
        }

        //CheckGameState();
        //CheckScene();
        //set in update so it is up-to-date with the people eaten
        //might have to add a people eaten counter to the feeding script
        if (PlayerController.Instance.GetComponent<Feeding>() != null)
            peopleEaten = PlayerController.Instance.GetComponent<Feeding>().currentHunger;
            //peopleEaten = player.GetComponent<Feeding>().currentHunger;


        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(peopleEaten + "/" + peopleEatingThreshold);
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "Spawn":
                if (PlayerController.Instance != null)
                {
                    foreach (BoxCollider2D boxCollider in PlayerController.Instance.GetPlayerDeath().boxColliders)
                    {
                        if (boxCollider.isTrigger)
                            boxCollider.enabled = true;
                    }
                    if (PlayerController.Instance.GetPlayerDeath().isInvincible)
                    {
                        PlayerController.Instance.GetPlayerDeath().isInvincible = false;
                        PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
                        PlayerController.Instance.GetPlayerDeath().healthBarScript.SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
                        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().UpdateHealthBarColour();
                        CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().setMaxHealth(100);
                    }
                }

                if (pauseMenu == null && !hasPaused)
                {
                    pauseMenu = FindFirstObjectByType<PauseMenu>().gameObject;
                    hasPaused = true;
                    if (pauseMenu != null)
                    {
                        FindFirstObjectByType<CoffinInteraction>().enabled = false;
                        StartCoroutine(CutsceneDelay(9.5f));
                    }
                }
                if (timer != null)
                {
                    timer.SetIsRotating(false);
                    timer.enabled = false;
                }

                CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);


                break;
            case "Level 1":
                hasPaused = false;
                nextLevel = "Level 2";
                currentLevel = "Level 1";
                //currentGameState = GameStates.PlayerInLevel;
                if (timer != null)
                {
                    timer.enabled = true;
                    timer.SetIsRotating(true);
                }

                CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
                break;
            case "Level 2":
                hasPaused = false;
                nextLevel = "BossLevel";
                currentLevel = "Level 2";
                if (timer != null)
                {
                    timer.enabled = true;
                    timer.SetIsRotating(true);
                }

                CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
                break;
            case "BossLevel":
                hasPaused = false;
                nextLevel = "Spawn";
                currentLevel = "Level 1"; 
                if(timer != null)
                {
                    timer.enabled = false;
                    timer.SetIsRotating(false); 
                }

                CanvasManager.Instance.HealthBar.GetComponent<NewHealthBarScript>().SetHealth(PlayerController.Instance.GetPlayerDeath().currentHealth);
                break;
            case "Main Menu Animated":
                hasPaused = false;
                break;
            case "Tutorial_Level":
                hasPaused = false;

                if (timer != null)
                {
                    timer.SetIsRotating(false);
                    timer.enabled = false;
                }
                break;
            default:
                break;
        }
    }

    private void EnableBoxCollider()
    {
        foreach (BoxCollider2D boxCollider in PlayerController.Instance.GetPlayerDeath().boxColliders)
        {
            if (boxCollider.isTrigger)
                boxCollider.enabled = true;
        }
    }

    private IEnumerator CutsceneDelay(float delay)
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        PlayerController.Instance.transform.position = new(-100, -100, 0);

        yield return new WaitForSeconds(delay);

        PlayerController.Instance.transform.position = new(-1.84f, 0.52f, 0);

        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        FindFirstObjectByType<CoffinInteraction>().enabled = true;
    }

    private void CheckScene()
    {
        var scene = SceneManager.GetActiveScene();
        var spawn = SceneManager.GetSceneByName("Spawn");

        if (scene == spawn)
        {
            //Do not start the game - no timer
            if (timer != null)
                timer.enabled = false;
        }
        //else if (scene == SceneManager.GetSceneByName("Main Menu Animated"))
        //{
        //    if (GameObject.FindObjectOfType<CountdownTimer>() != null)
        //    {
        //        GameObject GO = GameObject.FindObjectOfType<CountdownTimer>().transform.parent.gameObject;
        //        Destroy(GO);
        //    }
        //}
        //else if(scene == SceneManager.GetSceneByName("Level 2"))
        //{
        //    if(timer != null)
        //    {
        //        timer.resetTimer(); 
        //        timer.enabled = true;
        //    }
        //   
        //}
        //else
        //{
        //    if (timer != null)
        //        timer.enabled = true;
        //    // player = GameObject.FindGameObjectWithTag("Player");
        //}
    }

    public void ChangeGameState(GameStates newState)
    {
        currentGameState = newState;
    }

    public GameStates GetCurrentGameState()
    {
        return currentGameState;
    }

    public bool CanChangeLevel()
    {
        return canChangeLevel;
    }

    //void CheckGameState()
    //{
    //    //if player dies, the game state will be updated accordingly
    //    if(player.GetComponent<PlayerDeath>().currentHealth <= 0)
    //    {
    //        ChangeGameState(GameStates.PlayerDead);
    //    }
    //    else if(player.GetComponent<PlayerDeath>().godMode == true)
    //    {
    //        ChangeGameState(GameStates.GodMode);
    //    }
    //    //checks if player is out of time
    //    if(timer.timeRemaining == 0)
    //    {
    //        ChangeGameState(GameStates.ObjectiveFailed);
    //        canChangeLevel = false;
    //    }
    //    if(peopleEaten >= peopleEatingThreshold && timer.timeRemaining >= 0)
    //    {
    //        ChangeGameState(GameStates.ObjectiveCompleted); 
    //        canChangeLevel = true;
    //    }

    //}

}
