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
    private float minRunDistance = 4;
    private float maxRunDistance = 6;
    private float runOffset = 0.05f;  // Stops them from running straight
    private float minCheckTime = 0.5f;
    private float maxCheckTime = 1.5f;
    private float stopDistance = 0.5f;  // When they start slowing down
    private bool debugPath = false;

    private Vector2 runDestination = Vector2.zero;
    private PathfindingSmoothing path;
    private int pathErrorCheck;
    private int pathIndex = 0;
    private float speedPercent;

    public float checkTime;


    public RunState()
    {
        checkTime = 0f;
    }


    public override void UpdateLogic()
    {

        //change colour to indicate state change
        this.GetComponent<SpriteRenderer>().color = Color.blue;

        if (checkTime > 0)  // Wait a bit before running 
            checkTime -= Time.deltaTime;

        else
        {
            if (path != null && path.turnBoundaries != null && path.turnBoundaries.Length != 0)
            {
                if (path.turnBoundaries[pathIndex].HasCrossedLine(transform.position) || speedPercent < 0.1f)
                {
                    if (pathIndex == path.finishLineIndex)  // Has finished
                    {
                        path = new PathfindingSmoothing(null, Vector3.zero, 0, 0);
                        runDestination = Vector2.zero;  // Stop to look around and see if they escaped
                        checkTime = Random.Range(minCheckTime, maxCheckTime);
                        FindWalkTarget();
                        character.isMoving = false;
                        return;
                    }
                    else  // Has reached a checkpoint
                        pathIndex++;
                }

                Quaternion targetRotation = Quaternion.identity;
                if (speedPercent > 0.25f)
                {
                    Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (path.lookPoints[pathIndex] - transform.position);  // Direction towards the target location
                    targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);  // Get the direction as a quaternion
                    Quaternion rotateBy = pathIndex == 0 ? Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed * 5):  // If the AI has just stared moving
                                                           Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed * 2);  
                    transform.rotation = Quaternion.AngleAxis(rotateBy.eulerAngles.z, Vector3.forward);  // Turn the character
                }

                if (pathIndex >= path.slowDownIndex && stopDistance > 0)
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(transform.position) / stopDistance);  // Slow the character down near the end of the path

                transform.Translate(Vector3.right * character.runSpeed * speedPercent * Time.deltaTime, Space.Self);  // Move the character forwards
            }
            else
                FindWalkTarget();

            character.isMoving = true;
        }
    }


    private void FindWalkTarget()
    {
        Vector3 moveVector = character.GetPlayerPosition() - character.transform.position;
        moveVector = moveVector.normalized;
        runDestination = new Vector3((-moveVector.x + Random.Range(-runOffset, runOffset)) * Random.Range(minRunDistance, maxRunDistance),
                                     -moveVector.y + Random.Range(-runOffset, runOffset)) * Random.Range(minRunDistance, maxRunDistance);

        PathfindingRequestManager.RequestPath(transform.position, runDestination, this, OnPathFound);
        pathErrorCheck++;
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathfindingSmoothing(waypoints, transform.position, character.turnDistance, stopDistance);
            pathIndex = 0;
            speedPercent = 1;
            pathErrorCheck = 0;
        }
        else if (pathErrorCheck > 250)
        {
            Debug.Log(character.transform.name + " Run state pathfinding error");
            if (PathfindingRequestManager.requestListSize < 5)
                FindWalkTarget();
        }
        else
            FindWalkTarget();  // Try and find a new path
    }

    public void OnDrawGizmos()
    {
        if (path != null && path.turnBoundaries != null && debugPath)
            path.DrawWithGizmos();
    }
}
