using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HuntState : StateBaseClass
{
    public float pathRefreshTime = 1f;  // How often a new path to the player should be found
    private float refreshTimer = 0;
    private bool debugPath = false;

    private PathfindingSmoothing path;
    private int pathIndex = 0;
    private int pathErrorCheck;


    public override void UpdateLogic()
    {

        //change colour to indicate state change
        this.GetComponent<SpriteRenderer>().color = Color.magenta;


        refreshTimer -= Time.deltaTime;

        if (refreshTimer <= 0)
        {
            refreshTimer = pathRefreshTime;
            PathfindingRequestManager.RequestPath(transform.position, character.player.transform.position, this, OnPathFound);
            pathErrorCheck++;
        }


        if (path != null && path.turnBoundaries != null && path.turnBoundaries.Length != 0)
        {
            while (path.turnBoundaries[pathIndex].HasCrossedLine(transform.position))
            {
                if (pathIndex == path.finishLineIndex)  // Has finished
                {
                    path = new PathfindingSmoothing(null, Vector3.zero, 0, 0);
                    refreshTimer = 0;
                    return;
                }
                else  // Has reached a checkpoint
                    pathIndex++;
            }

            Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (path.lookPoints[pathIndex] - transform.position);  // Direction towards the target location
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);  // Get the direction as a quaternion
            Quaternion rotateBy = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed * 2);  // Turn towards it slowly
            transform.rotation = Quaternion.AngleAxis(rotateBy.eulerAngles.z, Vector3.forward);  // Turn the character

            transform.Translate(Vector3.right * character.runSpeed * Time.deltaTime, Space.Self);  // Move the character forwards
        }
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathfindingSmoothing(waypoints, transform.position, character.turnDistance, 0);
            pathIndex = 0;
            pathErrorCheck = 0;
            character.isMoving = true;
        }
        else if (pathErrorCheck > 250)
        {
            Debug.Log(character.transform.name + " Hunt state pathfinding error");
            if (PathfindingRequestManager.requestListSize < 5)
                refreshTimer = 0;
            else
                refreshTimer = 10;
        }
        else
            refreshTimer = 0;  // Try and find a new path
    }

    public void OnDrawGizmos()
    {
        if (path != null && path.turnBoundaries != null && debugPath)
            path.DrawWithGizmos();
    }
}
