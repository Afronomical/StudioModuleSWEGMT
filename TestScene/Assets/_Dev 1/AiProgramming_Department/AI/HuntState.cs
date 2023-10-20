using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HuntState : StateBaseClass
{
    public float pathRefreshTime = 1;  // How often a new path to the player should be found
    private float refreshTimer = 0;
    private Vector3[] path;
    private Vector3 currentWaypoint;
    private int targetPathIndex;

    public override void UpdateLogic()
    {
        refreshTimer -= Time.deltaTime;

        if (refreshTimer <= 0)
        {
            refreshTimer = pathRefreshTime;
            PathfindingRequestManager.RequestPath(transform.position, character.player.transform.position, this, OnPathFound);
        }


        if (path != null)
        {
            if (transform.position == currentWaypoint)
            {
                targetPathIndex++;
                if (targetPathIndex >= path.Length)
                {
                    path = new Vector3[0];
                    targetPathIndex = 0;
                    return;
                }
                currentWaypoint = path[targetPathIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, character.runSpeed * Time.deltaTime);
        }
    }


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && newPath.Length != 0)
        {
            path = newPath;
            currentWaypoint = path[0];  // Set the first waypoint
        }
        else
            refreshTimer = 0;  // Try and find a new path
    }
}
