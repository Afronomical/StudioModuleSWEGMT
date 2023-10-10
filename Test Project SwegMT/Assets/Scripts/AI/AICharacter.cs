/*
 *This script should be placed on the AI character
 *It is responsible for the character's main logic
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
        Attack,
        Run
    }


    [Header("Character Stats")]
    public CharacterTypes characterType;
    public int health = 3;
    public float walkSpeed, runSpeed;

    [Header("States")]
    public States currentState;
    public StateBaseClass stateScript;



    void Start()
    {
        ChangeState(States.Idle);  // The character will start in the idle state
    }


    void Update()
    {
        stateScript.UpdateLogic();  // Calls the virtual function for whatever state scripts
    }


    public StateBaseClass GetCurrentState()  // Tell the script that called it which state is currently active
    {
        return stateScript;
    }


    public void ChangeState(States newState)  // Will destroy the old state script and create a new one
    {
        if (currentState != newState || stateScript == null)
        {
            if (stateScript != null)
            {
                //----------------------------------------- Destroy script here
            }

            currentState = newState;

            switch (newState)
            {
                case States.Idle:
                    stateScript = new IdleState();
                    break;
                case States.Patrol:
                    stateScript = new PatrolState();
                    break;
                case States.Run:
                    stateScript = new RunState();
                    break;
                case States.Attack:
                    stateScript = new AttackState();
                    break;
                case States.Downed:
                    stateScript = new DownedState();
                    break;
                case States.Dead:
                    stateScript = new DeadState();
                    break;

                //------------------------------------ Add new states in here

                default:
                    stateScript = new IdleState();
                    break;
            }

            stateScript.character = this;  // Set the reference that state scripts will use
            //stateScript.player = GameObject.FindGameObjectWithTag("Player");
        }
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }


    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
