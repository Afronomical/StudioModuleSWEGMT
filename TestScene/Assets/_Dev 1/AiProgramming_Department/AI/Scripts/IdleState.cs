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
    private float minIdleTime = 2;
    private float maxIdleTime = 10;
    private float maxWalkDistance = 5;  // How far the character can walk while idle
    private float stopDistance = 1;  // When they start slowing down
    private bool debugPath = false;

    private PathfindingSmoothing path;
    private int pathIndex = 0;
    private int pathErrorCheck;
    private float speedPercent;


    public IdleState()
    {
        idleTime = 2f;
        walking = false;
    }


    public override void UpdateLogic()
    {
        //change colour to indicate state change
        this.GetComponent<SpriteRenderer>().color = Color.green;

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
            character.isMoving = true;
            FindWalkTarget();
        }
    }


    private void Walk()
    {
        if (path != null && path.turnBoundaries != null && path.turnBoundaries.Length != 0)
        {
            while (path.turnBoundaries[pathIndex].HasCrossedLine(transform.position) || speedPercent < 0.1f)
            {
                if (pathIndex == path.finishLineIndex)  // Has finished
                {
                    path = new PathfindingSmoothing(null, Vector3.zero, 0, 0);
                    walking = false;
                    idleTime = Random.Range(minIdleTime, maxIdleTime);  // How long the character will stand still for
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
                Quaternion rotateBy = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed);  // Turn towards it slowly
                transform.rotation = Quaternion.AngleAxis(rotateBy.eulerAngles.z, Vector3.forward);  // Turn the character
            }

            if (pathIndex >= path.slowDownIndex && stopDistance > 0)
                speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(transform.position) / stopDistance);  // Slow the character down near the end of the path

            if (pathIndex == 0 && transform.rotation.eulerAngles.z > targetRotation.eulerAngles.z - 15f && transform.rotation.eulerAngles.z < targetRotation.eulerAngles.z + 15f)  // If they are just starting to move then turn on the spot
                transform.Translate(Vector3.right * character.walkSpeed * speedPercent * Time.deltaTime, Space.Self);  // Move the character forwards
            else if (pathIndex != 0)
                transform.Translate(Vector3.right * character.walkSpeed * speedPercent * Time.deltaTime, Space.Self);  // Move the character forwards
        }
    }


    private void FindWalkTarget()
    {
        walkDestination = new Vector3(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y + Random.Range(-maxWalkDistance, maxWalkDistance));
        PathfindingRequestManager.RequestPath(transform.position, walkDestination, this, OnPathFound);
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
            Debug.Log(character.transform.name + " Idle state pathfinding error");
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
