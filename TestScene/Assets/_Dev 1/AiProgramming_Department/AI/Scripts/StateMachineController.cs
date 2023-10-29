using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
//using "Character"

public class StateMachineController : MonoBehaviour
{
    //handles the switching of the states depending on if certain conditions are met
    private AICharacter character;
    public float detectionRange = 4f;
    public float attackRange = 1f;

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

        float distance = Vector3.Distance(character.player.transform.position, character.transform.position);
        //Debug.Log(distance);

        if (character.characterType == AICharacter.CharacterTypes.Villager)
        {
            if (distance < detectionRange)
            {
                character.ChangeState(AICharacter.States.Run);

            }
            else
            {
                character.ChangeState(AICharacter.States.Idle);
            }
        }
        else if (character.characterType == AICharacter.CharacterTypes.Hunter)
        {
            //patrol if out of detection range
            if (distance > detectionRange)
            {
                character.ChangeState(AICharacter.States.Patrol);
            }
            //hunt while not in attack range
            else if(distance < detectionRange && distance > attackRange)
            {
                character.ChangeState(AICharacter.States.Hunt);
            }
            //attack when in attack range
            else
            {
                character.ChangeState(AICharacter.States.Attack);
            }
        }
        else if(character.characterType == AICharacter.CharacterTypes.RangedHunter)
        {
            if(distance < attackRange)
            {
                character.ChangeState(AICharacter.States.Shoot);
            }
            else if(distance < detectionRange && distance > attackRange)
            {
                character.ChangeState(AICharacter.States.Hunt);
            }
            
            else if(distance > detectionRange)
            {
                character.ChangeState(AICharacter.States.Patrol);
            }
        }
    }
}
