/*
 *This script is created by AICharacter, it inherits from StateBaseClass
 *It controls the behaviour of the villager when they are running away
 * 
 * Written by Aaron
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBaseClass
{
    private float minRunDistance = 2;
    private float maxRunDistance = 3;
    private float runOffset = 2;  // Stops them from running straight
    private float minCheckTime = 2;
    private float maxCheckTime = 5;

    private Vector3 runDestination = Vector3.zero;
    private float checkTime;


    public RunState()
    {
        checkTime = 1f;
    }


    public override void UpdateLogic()
    {
        if (checkTime > 0)  // Wait a bit before running 
            checkTime -= Time.deltaTime;

        else
        {
            if (runDestination == Vector3.zero)
            {
                Vector3 moveVector = character.GetPlayerPosition() - character.transform.position;
                runDestination = new Vector3((-moveVector.x + Random.Range(-runOffset, runOffset))* Random.Range(minRunDistance, maxRunDistance), 
                                             character.GetPosition().y, 
                                             (-moveVector.z + Random.Range(-runOffset, runOffset)) * Random.Range(minRunDistance, maxRunDistance));
            }

            character.SetPosition(Vector3.MoveTowards(character.GetPosition(), runDestination, character.runSpeed * Time.deltaTime));  // Move towards the destination

            if (Vector3.Distance(character.GetPosition(), runDestination) <= 0.01f)  // When they reach the destination
            {
                // Stop to look around and see if they escaped
                runDestination = Vector3.zero;
                checkTime = Random.Range(minCheckTime, maxCheckTime);
            }
        }
    }
}
