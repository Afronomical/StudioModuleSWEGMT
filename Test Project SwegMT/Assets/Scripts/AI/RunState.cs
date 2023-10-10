using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBaseClass
{
    private Vector3 runDestination;

    public RunState()
    {
        Vector3 moveVector = character.transform.position - character.GetPlayerPosition();
        runDestination = moveVector * -5;
    }


    public override void UpdateLogic()
    {
        character.SetPosition(Vector3.MoveTowards(character.GetPosition(), runDestination, character.walkSpeed * Time.deltaTime));
    }
}
