/*
 *This script should be placed on the AI character
 *It is responsible for the character's main logic
 * 
 * Written by Aaron & Adam
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject player;



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
                    stateScript = transform.AddComponent<IdleState>();
                    break;
                case States.Patrol:
                    stateScript = transform.AddComponent<PatrolState>();
                    break;
                case States.Run:
                    stateScript = transform.AddComponent<RunState>();
                    break;
                case States.Attack:
                    stateScript = transform.AddComponent<AttackState>();
                    break;
                case States.Downed:
                    stateScript = transform.AddComponent<DownedState>();
                    break;
                case States.Dead:
                    stateScript = transform.AddComponent<DeadState>();
                    break;

                //------------------------------------ Add new states in here

                default:
                    stateScript = transform.AddComponent<IdleState>();
                    break;
            }

            stateScript.character = this;  // Set the reference that state scripts will use
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


    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
}
