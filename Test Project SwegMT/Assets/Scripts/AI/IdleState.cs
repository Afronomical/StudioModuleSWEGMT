using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleState : StateBaseClass
{
    private bool walking;
    private Vector3 walkDestination;
    private float idleTime;
    private float maxWalkDistance = 5;  // How far the character can walk while idle


    public IdleState()
    {
        idleTime = Random.Range(2, 10);
        walking = false;
    }


    public override void UpdateLogic()
    {
        if (walking)
            Walk();
        else
            Idle();
    }


    private void Idle()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0)
        {
            walking = true;
            walkDestination = new Vector3(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y,
                                          character.GetPosition().z + Random.Range(-maxWalkDistance, maxWalkDistance));
        }
    }


    private void Walk()
    {
        character.SetPosition(Vector3.MoveTowards(character.GetPosition(), walkDestination, character.walkSpeed * Time.deltaTime));

        if (Vector3.Distance(character.GetPosition(), walkDestination) <= 0.01f)
        {
            walking = false;
            idleTime = Random.Range(2, 10);  // How long the character will stand still for
        }
    }
}
