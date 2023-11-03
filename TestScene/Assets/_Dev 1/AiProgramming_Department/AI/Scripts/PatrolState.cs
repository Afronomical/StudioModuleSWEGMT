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
    private float minWalkDistance = 6;
    private float maxWalkDistance = 12;  // How far the character can walk while idle
    private float minIdleTime = 0;
    private float maxIdleTime = 3;
    private float stopDistance = 0.25f;  // When they start slowing down
    private bool debugPath = false;

    private PathfindingSmoothing path;
    private int pathIndex = 0;
    private int pathErrorCheck;
    private float speedPercent;

    public override void UpdateLogic()
    {
        if(character.characterType == AICharacter.CharacterTypes.Hunter)
        {
            //change colour to indicate state change
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }   
        else if(character.characterType == AICharacter.CharacterTypes.RangedHunter)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
            

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
            character.isMoving = true;
            FindWalkTarget();
        }
    }


    private void Patrol()
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
        pathErrorCheck++;
        walkDestination = new Vector2(character.GetPosition().x + Random.Range(-maxWalkDistance, maxWalkDistance), character.GetPosition().y + Random.Range(-maxWalkDistance, maxWalkDistance));
        if (Vector3.Distance(character.GetPosition(), walkDestination) < minWalkDistance)
            FindWalkTarget();
        else
            PathfindingRequestManager.RequestPath(transform.position, walkDestination, this, OnPathFound);
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
            Debug.Log(character.transform.name + " Patrol state pathfinding error");
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
