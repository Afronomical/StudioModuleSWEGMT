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
    private float changeStateTime = 0.5f;


    private void Start()
    {
        character = GetComponent<AICharacter>();
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
        lastPosition = transform.position;


        if (character.health == 0)
            character.ChangeState(AICharacter.States.Dead);


        else if (character.health == 1)
            character.ChangeState(AICharacter.States.Downed);

        

        else if (character.characterType == AICharacter.CharacterTypes.Villager)
            VillagerStates();

        else if (character.characterType == AICharacter.CharacterTypes.Hunter)
            HunterStates();
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

            if (GetComponent<RunState>().checkTime > 0 && (distance > detectionRange * 1.5f || RaycastToPlayer(detectionRange * 1.5f)))  // Only change state when they stop running
                character.ChangeState(AICharacter.States.Idle);
            
            if (!StuckCheck())  // If the character is stuck on an object
                character.ChangeState(AICharacter.States.Idle);
        }


        else
            character.ChangeState(AICharacter.States.Idle);
    }


    private void HunterStates()
    {
        if (distance > detectionRange)  // Patrol if out of detection range
            character.ChangeState(AICharacter.States.Patrol);


        else if (distance < detectionRange && distance > attackRange)  // Hunt while not in attack range
        {
            if (character.currentState != AICharacter.States.Hunt)  // If they are running
                if (RaycastToPlayer(detectionRange))  // Can they see the player
                    character.ChangeState(AICharacter.States.Hunt);
            else  // If hunt is already the state then don't check for walls
                character.ChangeState(AICharacter.States.Hunt);
        }


        else if (distance < attackRange) // Attack when in attack range
            character.ChangeState(AICharacter.States.Attack);


        else if (!StuckCheck())  // If the character is stuck on an object
            character.ChangeState(AICharacter.States.Patrol);
    }


    private bool RaycastToPlayer(float range)
    {
        if (Physics2D.Raycast(transform.position, (character.player.transform.position - transform.position), range, unwalkableLayer))
            return false;  // The raycast hit a wall
        else return true;  // The enemy can see the player
    }


    private bool StuckCheck()
    {
        if (Vector3.Distance(lastPosition, transform.position) > 0.35f)  // They are moving
        {
            stuckCheckFrames = 0;
            return true;
        }
        else  // They haven't moved enough
        {
            stuckCheckFrames++;
            if (stuckCheckFrames > 10)
                return false;  // They are stuck
            else
                return true;  // They haven't been stuck long enough
        }
    }
}
