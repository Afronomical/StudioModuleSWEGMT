using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
//using "Character"

public class StateMachineController : MonoBehaviour
{
    //handles the switching of the states depending on if certain conditions are met
    public GameObject player;

    private void Start()
    {
        
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


        //placeholder value for reaction range
        int n = 0;

        if (this.CompareTag("Villager"))
        {
            if (this.transform.position.x < n)
            {
                /*
                 character.SetState(RunState);
                 character.GetCurrentState().UpdateLogic();
                 */
            }
            else
            {
                /*
                 character.SetState(IdleState);
                 character.GetCurrentState().UpdateLogic();
                 */
            }
        }
        else if (this.CompareTag("Hunter"))
        {
            if (this.transform.position.x > n)
            {
                /*
                 character.SetState(PatrolState);
                 character.GetCurrentState().UpdateLogic();
                 */
            }
            else
            {
                /*
                 character.SetState(AttackState);
                 character.GetCurrentState().UpdateLogic();
                 */
            }
        }
    }
}
