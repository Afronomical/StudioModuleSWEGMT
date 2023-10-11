using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleState : StateBaseClass
{
    public override void UpdateLogic()
    {
        //this is where the logic for idle state will happen
        Debug.Log("Is idle");
    }
}
