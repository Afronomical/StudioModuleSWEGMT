using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : StateBaseClass
{
    public override void UpdateLogic()
    {
        Debug.Log("Is dead");
    }
}