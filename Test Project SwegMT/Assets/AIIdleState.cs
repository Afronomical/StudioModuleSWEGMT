/*
 *This script will be created by AICharacter when the AI enters this state
 *
 * 
 * Written by Aaron & Adam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIBaseState
{
    public AIIdleState()  // Will run when the character enters this state
    {
        
    }

    public override void UpdateLogic()
    {
        Debug.Log("Idle");
    }

    ~AIIdleState()  // Will run when the character leaves this state
    {

    }
}
