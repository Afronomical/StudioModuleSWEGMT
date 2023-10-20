/*
 *This script is created by AICharacter, it inherits from StateBaseClass
 *It controls the behaviour of the enemy when they are idle
 *They will stand still and sometimes wander around
 * 
 * Written by Aaron and Adam
 */

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
    private Vector3[] path;
    private Vector3 currentWaypoint;
    private int targetPathIndex;


    public IdleState()
    {
        idleTime = 2f;
        walking = false;
    }


    public override void UpdateLogic()
    {
        //Debug.Log("Idle");
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
            FindWalkTarget();
        }
    }


    private void Walk()
    {
        if (path != null)
        {
            if (transform.position == currentWaypoint)
            {
                targetPathIndex++;
                if (targetPathIndex >= path.Length)
                {
                    path = new Vector3[0];
                    walking = false;
                    targetPathIndex = 0;
                    idleTime = Random.Range(2, 10);  // How long the character will stand still for
                    return;
                }
                currentWaypoint = path[targetPathIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, character.walkSpeed * Time.deltaTime);
        }
    }


    private void FindWalkTarget()
    {
        walkDestination = new Vector3(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y + Random.Range(-maxWalkDistance, maxWalkDistance));
        PathfindingRequestManager.RequestPath(transform.position, walkDestination, OnPathFound);
    }


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && newPath.Length != 0)
        {
            path = newPath;
            currentWaypoint = path[0];  // Set the first waypoint
        }
        else
            FindWalkTarget();  // Try and find a new path
    }
}
