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
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerIsDead;
    public bool canChangeLevel;

    

    //this is where the conditions that trigger state changes are defined
    //they are simple for now, subject to change as per Tech Design requirements
    public int peopleEatingThreshold;
    public int peopleEaten;


    public GameObject player;
    public Transform playerSpawn;
    private CountdownTimer timer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        }
    }

    public enum GameStates
    {
        //add whichever states to this
        PlayerDead,
        ObjectiveCompleted,//objective has to be clearly defined
        ObjectiveFailed,
        PlayerInLevel,
        GodMode
    }

    public GameStates currentGameState;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Instantiate(player, playerSpawn);

        //get reference to player
        player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);

        //reference to game timer
        timer = GameObject.Find("Countdown Text").GetComponent<CountdownTimer>();
        //default state
        currentGameState = GameStates.PlayerInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGameState();
        CheckScene();
        //set in update so it is up-to-date with the people eaten
        //might have to add a people eaten counter to the feeding script
        peopleEaten = Feeding.currentHunger;
    }

    private void CheckScene()
    {
        var scene = SceneManager.GetActiveScene();
        var spawn = SceneManager.GetSceneByName("Spawn");

        if(scene == spawn)
        {
            //Do not start the game - no timer
            timer.enabled = false;
        }
        else
        {
            timer.enabled = true;
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
