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
        //switch between states based on the pre-set conditions
        //need to liaise with leads
        /*if(this.getHealth() < n)
        {
            character.SetState(DownedState);
            character.GetCurrentState().UpdateLogic();
        }

        if(this.GetHealth() == 0)
        {
            character.SetState(DeadState);
            character.GetCurrentState().UpdateLogic();
        }
        */

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


        //put everything in separate functions
        if (character.characterType == AICharacter.CharacterTypes.Villager)
        {
            if (character.player.transform.position.x > this.transform.position.x + 2f)
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
            if (character.player.transform.position.x < this.transform.position.x + 2f)
            {
                character.ChangeState(AICharacter.States.Patrol);
            }
            else
            {
                character.ChangeState(AICharacter.States.Attack);
            }
        }
    }
}
