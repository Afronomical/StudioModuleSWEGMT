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
    private float distance;


    private void Start()
    {
        character = GetComponent<AICharacter>();
    }


    private void Update()
    {
        CheckState();
    }


    private void CheckState()
    {
        distance = Vector3.Distance(character.player.transform.position, character.transform.position);

        if (character.health == 0)
        {
            character.ChangeState(AICharacter.States.Dead);
            return;
        }

        if (character.health == 1)
        {
            character.ChangeState(AICharacter.States.Downed);
            return;
        }

        

        if (character.characterType == AICharacter.CharacterTypes.Villager)
        {
            VillagerStates();
        }

        else if (character.characterType == AICharacter.CharacterTypes.Hunter)
        {
            HunterStates();
        }
    }


    private void VillagerStates()
    {
        if (distance < detectionRange && RaycastToPlayer(detectionRange))  // When the player gets close to the villager
            character.ChangeState(AICharacter.States.Run);


        else if (character.currentState == AICharacter.States.Run)  // If they are running
            if (GetComponent<RunState>().checkTime > 0 && distance < detectionRange * 1.5f && RaycastToPlayer(detectionRange * 1.5f))  // Only change state when they stop running
                character.ChangeState(AICharacter.States.Idle);


            else
                character.ChangeState(AICharacter.States.Idle);
    }


    private void HunterStates()
    {
        if (distance > detectionRange && RaycastToPlayer(detectionRange))  // Patrol if out of detection range
            character.ChangeState(AICharacter.States.Patrol);


        else if (distance < detectionRange && distance > attackRange)  // Hunt while not in attack range
            if (RaycastToPlayer(detectionRange))
                character.ChangeState(AICharacter.States.Hunt);


        else  // Attack when in attack range
            character.ChangeState(AICharacter.States.Attack);
    }


    private bool RaycastToPlayer(float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (character.player.transform.position - transform.position), out hit, range))
        {
            if (hit.transform.gameObject.layer == 6)  // If it hits a wall (Unwalkable layer)
                return false;
            else return true;
        }
        return false;
    }
}
