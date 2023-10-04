/*
 *This script should be placed on the AI character
 *It is responsible for
 * 
 * Written by Aaron & Adam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    public enum CharacterTypes
    {
        Villager,
        Hunter
    }

    public enum States
    {
        Idle,
        Roam,
        Downed,
        Dead,
        Patrol,
        Attack
    }


    [Header("Character Stats")]
    public CharacterTypes characterType;
    public int health;
    public float walkSpeed, runSpeed;

    [Header("States")]
    public States currentState;
    public AIBaseState stateScript;




    void Start()
    {
        ChangeState(States.Idle);  // The character will start in the idle state
    }


    void Update()
    {
        stateScript.UpdateLogic();  // Calls the virtual function for whatever state scripts
    }


    public AIBaseState GetCurrentState()  // Tell the script that called it which state is currently active
    {
        return stateScript;
    }


    public void ChangeState(States newState)  // Will destroy the old state script and create a new one
    {
        if (stateScript != null)
        {
            //----------------------------------------- Destroy script here
        }

        currentState = newState;

        switch(newState)
        {
            case States.Idle:
                stateScript = new AIIdleState();
                break;
            case States.Patrol:
                stateScript = new AIPatrolState();
                break;

            //------------------------------------ Add new states in here

            default:
                stateScript = new AIIdleState(); 
                break;
        }
    }


    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
