using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIBaseState
{
    public AIPatrolState()
    {
        
    }

    public override void UpdateLogic()
    {
        Debug.Log("Patrolling");
    }
}
