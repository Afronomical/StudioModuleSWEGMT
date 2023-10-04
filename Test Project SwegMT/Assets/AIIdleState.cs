using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AICharacterBaseClass
{
    public AIIdleState()
    {
        
    }

    public override void UpdateLogic()
    {
        Debug.Log("Idle");
    }

    public override void UpdatePhysics()
    {

    }
}
