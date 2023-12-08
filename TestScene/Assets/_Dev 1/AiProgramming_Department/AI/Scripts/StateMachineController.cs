using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    // Handles the switching of the states depending on if certain conditions are met
    private AICharacter character;
    public float detectionRange = 4f;
    public float attackRange = 1f;
    public LayerMask unwalkableLayer;
    private float distance;
    private Vector3 lastPosition;
    private int stuckCheckFrames;
    private float changeStateTimer;
    private float changeStateTime = 0.2f;


    //ranged hunnter reloading values
    float currentTime;
    float reloadingTime = 5f;
    bool isReloading = false;

    private void Start()
    {
        character = GetComponent<AICharacter>();
        changeStateTimer = UnityEngine.Random.Range(-2.5f, changeStateTime);

        currentTime = reloadingTime;
    }


    private void Update()
    {
        changeStateTimer += Time.deltaTime;
        if (changeStateTimer > changeStateTime)  // Don't try to change the state every frame
        {
            changeStateTimer = 0f;
            CheckState();
        }
    }


    private void CheckState()
    {
        distance = Vector3.Distance(character.player.transform.position, character.transform.position);


        if (character.health <= 0)
            character.ChangeState(AICharacter.States.Dead);


        else if (character.health == 1)
            character.ChangeState(AICharacter.States.Downed);


        else if (!character.knowsAboutPlayer && distance < detectionRange && RaycastToPlayer(detectionRange))
            character.ChangeState(AICharacter.States.Alerted);


        else if (character.currentState != AICharacter.States.Alerted)
        {
            if (character.characterType == AICharacter.CharacterTypes.Villager)
                VillagerStates();


            else if (character.characterType == AICharacter.CharacterTypes.Hunter)
                HunterStates();


            else if (character.characterType == AICharacter.CharacterTypes.RangedHunter)
                RangedHunterStates();
        }
        


        if (character.isMoving && character.currentState != AICharacter.States.Run && character.currentState != AICharacter.States.Downed)  // Check to see if the character is stuck on an object
        {
            if (StuckCheck())
            {
                character.isMoving = false;
                character.ChangeState(AICharacter.States.None);
            }
        }
        else
            stuckCheckFrames = 0;

        lastPosition = transform.position;  // Update the last position of this character
    }


    private void VillagerStates()
    {
        if (character.currentState == AICharacter.States.Idle)
        {
            if (distance < detectionRange && RaycastToPlayer(detectionRange))  // When the player gets close to the villager
                character.ChangeState(AICharacter.States.Run);
        }


        else if (character.currentState == AICharacter.States.Run)  // If they are running
        {
            changeStateTimer = changeStateTime;  // Don't stop checking if the state can change

            if (!character.isMoving && distance > detectionRange * 1.5f && RaycastToPlayer(detectionRange * 1.5f))  // Only change state when they stop running
                character.ChangeState(AICharacter.States.Idle);
        }


        else
            character.ChangeState(AICharacter.States.Idle);
    }


    private void HunterStates()
    {
        if (distance > detectionRange)  // Patrol if out of detection range
            character.ChangeState(AICharacter.States.Patrol);


        else if (character.currentState == AICharacter.States.Attack && distance < attackRange * 2.5f)  // Attack when in attack range
            character.ChangeState(AICharacter.States.Attack);
        else if (distance < attackRange)
            character.ChangeState(AICharacter.States.Attack);


        else  // Hunt while not in attack range
        {
            if (character.currentState == AICharacter.States.Hunt)  // If hunt is already the state then don't check for walls
                character.ChangeState(AICharacter.States.Hunt);
            else if (RaycastToPlayer(detectionRange))  // Can they see the player
                character.ChangeState(AICharacter.States.Hunt);
        }
    }


    private void RangedHunterStates()
    {
        //if (character.reloading)
        //{
        //    character.ChangeState(AICharacter.States.Reload);
        //    //character.reloading = false;

        //}

        if (distance < attackRange && RaycastToPlayer(distance))
        {
            if (character.reloading)
            {
                character.ChangeState(AICharacter.States.Reload);
                //character.reloading = false;

            }
            else
            {
                character.ChangeState(AICharacter.States.Shoot);

            }

        }

        else if (distance < detectionRange)
        {
            character.ChangeState(AICharacter.States.Hunt);
        }

        else
        {
            character.ChangeState(AICharacter.States.Patrol);
        }
    }


    private bool RaycastToPlayer(float range)
    {
        if (Physics2D.Raycast(transform.position, (character.player.transform.position - transform.position), range, unwalkableLayer))
            return false;  // The raycast hit a wall
        else return true;  // The enemy can see the player
    }


    private bool StuckCheck()
    {
        if (Vector3.Distance(lastPosition, transform.position) > 0.2f)  // They are moving
        {
            stuckCheckFrames = 0;
            return false;
        }
        else  // They haven't moved enough
        {
            stuckCheckFrames++;
            if (stuckCheckFrames >= 4)
            {
                stuckCheckFrames = 0;
                return true;  // They are stuck
            }
            else
                return false;  // They haven't been stuck long enough
        }
    }
}
