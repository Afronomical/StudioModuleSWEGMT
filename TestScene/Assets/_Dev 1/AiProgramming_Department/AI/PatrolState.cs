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
        //if (path != null)
        //{
        //    if (transform.position == currentWaypoint)
        //    {
        //        targetPathIndex++;
        //        if (targetPathIndex >= path.Length)
        //        {
        //            path = new Vector3[0];
        //            walking = false;
        //            targetPathIndex = 0;
        //            idleTime = Random.Range(minIdleTime, maxIdleTime);  // How long the character will stand still for
        //            return;
        //        }
        //        currentWaypoint = path[targetPathIndex];
        //    }

        //    transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, character.walkSpeed * Time.deltaTime);
        //}
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
        }
        else
            FindWalkTarget();  // Try and find a new path
    }

    public void OnDrawGizmos()
    {
        if (path != null)
            path.DrawWithGizmos();
    }
}
