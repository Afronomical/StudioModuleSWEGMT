using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : StateBaseClass
{
    private bool walking;
    private Vector3 walkDestination;
    private float idleTime;
    private float maxWalkDistance = 5;  // How far the character can walk while idle

    public override void UpdateLogic()
    {
        if (walking)
            Patrol();
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


    private void Patrol()
    {
        character.SetPosition(Vector3.MoveTowards(character.GetPosition(), walkDestination, character.walkSpeed * Time.deltaTime));

        if (Vector3.Distance(character.GetPosition(), walkDestination) <= 0.01f)
        {
            walking = false;
            idleTime = Random.Range(2, 10);  // How long the character will stand still for
        }
    }
}
