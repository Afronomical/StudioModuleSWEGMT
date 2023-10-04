using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIBaseState
{
    public AIIdleState()
    {
        
    }

    public override void UpdateLogic()
    {
        Debug.Log("Idle");
    }
}
