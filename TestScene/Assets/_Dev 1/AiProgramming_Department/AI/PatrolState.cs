using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : StateBaseClass
{
    private bool walking;
    private Vector2 walkDestination;
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
            walkDestination = new Vector2(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y);
        }
    }


    private void Patrol()
    {
        character.SetPosition(Vector2.MoveTowards(character.GetPosition(), walkDestination, character.walkSpeed * Time.deltaTime));

        if (Vector2.Distance(character.GetPosition(), walkDestination) <= 0.01f)
        {
            walking = false;
            idleTime = Random.Range(2, 10);  // How long the character will stand still for
        }
    }
}
