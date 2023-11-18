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
        RangedHunter,
        Boss
    }

    public enum States
    {
        Idle,
        Downed,
        Dead,
        Patrol,
        Attack,
        Run,
        Hunt,
        Shoot,
        //boss states
        //phase 1, 2, 3...?
        SpecialAttack,
        SprayShoot1,
        SprayShoot2,
        CircularShoot,
        HomingArrow,
        SpawnEnemies,
        SprayArrows,
        Reload,
        SpinAttackBox,
        None
    }


    [Header("Character Stats")]
    public CharacterTypes characterType;
    public int startingHealth = 3;
    public int health;
    public int hungerValue = 1;
    public float walkSpeed, runSpeed, crawlSpeed;
    public float turnSpeed;
    public float turnDistance;

    [Header("States")]
    public States currentState;
    public StateBaseClass stateScript;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject homingBulletPrefab;
    public GameObject hunterPrefab;
    public GameObject archerPrefab;
    public GameObject spinattackboxPrefab;

    public bool isMoving;

    void Start()
    {
        walkSpeed /= 2;
        runSpeed /= 2;
        crawlSpeed /= 2;
        health = startingHealth;
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
            if (stateScript != null)
            {
                //destroy current script attached to AI character
                Destroy(stateScript);
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
                    AudioManager.Manager.PlaySFX("NPC_MeleeAttack");
                    stateScript = transform.AddComponent<AttackState>();
                    break;
                case States.Downed:
                    stateScript = transform.AddComponent<DownedState>();
                    break;
                case States.Dead:
                    AudioManager.Manager.PlaySFX("NPC_Death");
                    stateScript = transform.AddComponent<DeadState>();
                    break;
                case States.Hunt:
                    stateScript = transform.AddComponent<HuntState>();
                    break;
                case States.Shoot:
                    
                    stateScript = transform.AddComponent<ShootState>();
                    break;
                //boss states
                case States.SpecialAttack:
                    stateScript = transform.AddComponent<SpecialAttackState>();
                    break;
                case States.SprayShoot1:
                    stateScript = transform.AddComponent<SprayShoot1State>();
                    break;
                //case States.SprayShoot2:
                //    stateScript = transform.AddComponent<SprayShoot2State>();
                //    break;
                case States.CircularShoot:
                    stateScript = transform.AddComponent<CircularShootState>();
                    break;
                case States.HomingArrow:
                    stateScript = transform.AddComponent<HomingArrowState>();
                    break;
                case States.SprayArrows:
                    stateScript = transform.AddComponent<SprayArrowsState>();
                    break;
                case States.Reload:
                    stateScript = transform.AddComponent<ReloadState>();
                    break;
                case States.SpinAttackBox:
                    stateScript = transform.AddComponent<SpinAttackState>();
                    break;
                //case States.SpawnEnemies:
                //    stateScript = transform.AddComponent<SpawnEnemiesState>();
                //    break;
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
