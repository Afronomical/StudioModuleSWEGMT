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
        Hunter,
        RangedHunter
    }

    public enum States
    {
        None,
        Idle,
        Roam,
        Downed,
        Dead,
        Patrol,
        Attack,
        Run,
        Hunt,
        Shoot
    }


    [Header("Character Stats")]
    public CharacterTypes characterType;
    public int health = 3;
    public float walkSpeed, runSpeed, crawlSpeed;
    public float turnSpeed;
    public float turnDistance;

    [Header("States")]
    public States currentState;
    public StateBaseClass stateScript;
    public bool isMoving;

    public GameObject player;

    public GameObject bulletPrefab;

    void Start()
    {
        walkSpeed /= 2;
        runSpeed /= 2;
        crawlSpeed /= 2;
        ChangeState(States.Idle);  // The character will start in the idle state
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (stateScript != null)
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
            if (stateScript != null && currentState != States.None)
            {
                Destroy(stateScript);  // Destroy current script attached to AI character
            }

            //set the current state of AI character to the new state
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
                    AudioManager.Manager.PlayVFX("NPC_MeleeAttack");
                    stateScript = transform.AddComponent<AttackState>();
                    break;
                case States.Downed:
                    AudioManager.Manager.PlayVFX("NPC_Downed");
                    stateScript = transform.AddComponent<DownedState>();
                    break;
                case States.Dead:
                    AudioManager.Manager.PlayVFX("NPC_Death");
                    stateScript = transform.AddComponent<DeadState>();
                    break;
                case States.Hunt:
                    stateScript = transform.AddComponent<HuntState>();
                    break;
                case States.Shoot:
                    stateScript = transform.AddComponent<ShootState>();
                    break;
                //------------------------------------ Add new states in here

                case States.None:
                    stateScript = null;
                    break;
                default:
                    stateScript = transform.AddComponent<IdleState>();
                    break;
            }

            if (stateScript != null)
                stateScript.character = this;  // Set the reference that state scripts will use
        }
    }


    public Vector2 GetPosition()
    {
        return transform.position;
    }


    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }


    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public void SetHealth(int n)
    {
        this.health = n;
    }
}
