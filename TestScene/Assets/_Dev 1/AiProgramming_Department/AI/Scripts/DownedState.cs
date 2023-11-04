using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownedState : StateBaseClass
{
    private float minCrawlDistance = 1f;
    private float maxCrawlDistance = 6f;
    private float crawlOffset = 1.25f;  // Stops them from running straight
    private float minCheckTime = 3;
    private float maxCheckTime = 8;
    private float stopDistance = 1;  // When they start slowing down
    private bool debugPath = false;

    private PathfindingSmoothing path;
    private int pathIndex = 0;
    private float speedPercent;
    private Vector3 crawlDestination;
    private float checkTime;

    public DownedState()
    {
        checkTime = 0f;
    }

    private void Start()
    {
        //change colour to indicate state change
        transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.red;
    }


    public override void UpdateLogic()
    {

        if (checkTime > 0)  // Wait a bit before running 
            checkTime -= Time.deltaTime;

        else
        {
            if (path != null && path.turnBoundaries != null && path.turnBoundaries.Length != 0)
            {
                while (path.turnBoundaries[pathIndex].HasCrossedLine(transform.position) || speedPercent < 0.1f)
                {
                    if (pathIndex == path.finishLineIndex)  // Has finished
                    {
                        path = new PathfindingSmoothing(null, Vector3.zero, 0, 0);
                        crawlDestination = Vector2.zero;  // Stop to look around and see if they escaped
                        checkTime = Random.Range(minCheckTime, maxCheckTime);
                        character.isMoving = false;
                        FindWalkTarget();
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
                    transform.Translate(Vector3.right * character.crawlSpeed * speedPercent * Time.deltaTime, Space.Self);  // Move the character forwards
                else if (pathIndex != 0)
                    transform.Translate(Vector3.right * character.crawlSpeed * speedPercent * Time.deltaTime, Space.Self);  // Move the character forwards
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
        crawlDestination = new Vector3((-moveVector.x + Random.Range(-crawlOffset, crawlOffset)) * Random.Range(minCrawlDistance, maxCrawlDistance),
                                     -moveVector.y + Random.Range(-crawlOffset, crawlOffset)) * Random.Range(minCrawlDistance, maxCrawlDistance);

        PathfindingRequestManager.RequestPath(new PathRequest(transform.position, crawlDestination, this, OnPathFound));
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathfindingSmoothing(waypoints, transform.position, character.turnDistance, stopDistance);
            pathIndex = 0;
            speedPercent = 1;
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
