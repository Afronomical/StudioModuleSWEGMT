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
    private float minRunDistance = 3;
    private float maxRunDistance = 8;
    private float runOffset = 1.25f;  // Stops them from running straight
    private float minCheckTime = 1;
    private float maxCheckTime = 3;


    private Vector2 runDestination = Vector2.zero;
    private Vector3[] path;
    private Vector3 currentWaypoint;
    private int targetPathIndex;
    private float checkTime;


    public RunState()
    {
        checkTime = 0f;
    }


    public override void UpdateLogic()
    {
        if (checkTime > 0)  // Wait a bit before running 
            checkTime -= Time.deltaTime;

        else
        {
            if (path == null)
            {
                FindWalkTarget();
            }
            else
            {
                if (transform.position == currentWaypoint)
                {
                    targetPathIndex++;
                    if (targetPathIndex >= path.Length)
                    {
                        path = new Vector3[0];
                        targetPathIndex = 0;

                        runDestination = Vector2.zero;  // Stop to look around and see if they escaped
                        checkTime = Random.Range(minCheckTime, maxCheckTime);
                        FindWalkTarget();
                        return;
                    }
                    currentWaypoint = path[targetPathIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, character.runSpeed * Time.deltaTime);
            }
        }
    }


    private void FindWalkTarget()
    {
        Vector3 moveVector = character.GetPlayerPosition() - character.transform.position;
        moveVector = moveVector.normalized;
        runDestination = new Vector3((-moveVector.x + Random.Range(-runOffset, runOffset)) * Random.Range(minRunDistance, maxRunDistance),
                                     -moveVector.y + Random.Range(-runOffset, runOffset)) * Random.Range(minRunDistance, maxRunDistance);

        PathfindingRequestManager.RequestPath(transform.position, runDestination, this, OnPathFound);
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
