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
    private float maxWalkDistance = 10;  // How far the character can walk while idle
    private float minIdleTime = 0;
    private float maxIdleTime = 3;

    //private Vector3[] path;
    //private Vector3 currentWaypoint;
    //private int targetPathIndex;
    private PathfindingSmoothing path;
    private int pathIndex = 0;

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
            FindWalkTarget();
        }
    }


    private void Patrol()
    {
        if (path != null && path.turnBoundaries != null)
        {
            while (path.turnBoundaries[pathIndex].HasCrossedLine(transform.position))
            {
                if (pathIndex == path.finishLineIndex)  // Has finished
                {
                    path = new PathfindingSmoothing(null, Vector3.zero, 0);
                    walking = false;
                    idleTime = Random.Range(minIdleTime, maxIdleTime);  // How long the character will stand still for
                    return;
                }
                else  // Has reached a checkpoint
                    pathIndex++;
            }

            Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (path.lookPoints[pathIndex] - transform.position);  // Direction towards the target location
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);  // Get the direction as a quaternion
            Quaternion rotateBy = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed);  // Turn towards it slowly
            transform.rotation = Quaternion.AngleAxis(rotateBy.eulerAngles.z, Vector3.forward);  // Turn the character
            
            if (pathIndex == 0 && transform.rotation.eulerAngles.z > targetRotation.eulerAngles.z - 15f && transform.rotation.eulerAngles.z < targetRotation.eulerAngles.z + 15f)  // If they are just starting to move then turn on the spot
                transform.Translate(Vector3.right * character.walkSpeed * Time.deltaTime, Space.Self);  // Move the character forwards
            else if (pathIndex != 0)
                transform.Translate(Vector3.right * character.walkSpeed * Time.deltaTime, Space.Self);  // Move the character forwards
        }
    }


    private void FindWalkTarget()
    {
        walkDestination = new Vector2(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y + Random.Range(-maxWalkDistance, maxWalkDistance));
        PathfindingRequestManager.RequestPath(transform.position, walkDestination, this, OnPathFound);
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathfindingSmoothing(waypoints, transform.position, character.turnDistance);
            pathIndex = 0;
        }
        else
            FindWalkTarget();  // Try and find a new path
    }

    public void OnDrawGizmos()
    {
        if (path != null && path.turnBoundaries != null)
            path.DrawWithGizmos();
    }
}
