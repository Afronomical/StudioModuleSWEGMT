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
    private int pathfindingErrorCheck;
    private int errorMovement;
    private float bossHuntTime = 7.5f;
    private float bossHuntTimer;


    public override void UpdateLogic()
    {
        refreshTimer -= Time.deltaTime;

        if (errorMovement >= 1)
        {
            Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);  // Get the direction as a quaternion
            Quaternion rotateBy = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * character.turnSpeed * 2);  // Turn towards it slowly
            transform.rotation = Quaternion.AngleAxis(rotateBy.eulerAngles.z, Vector3.forward);  // Turn the character
            transform.Translate(Vector3.right * character.runSpeed * Time.deltaTime, Space.Self);  // Move the character forwards

            errorMovement++;
            if (errorMovement >= 50)
                errorMovement = 0;
        }
        else
        {
            if (refreshTimer <= 0)
            {
                refreshTimer = pathRefreshTime;
                PathfindingRequestManager.RequestPath(new PathRequest(transform.position, character.player.transform.position, this, OnPathFound));
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

            if (character.characterType == AICharacter.CharacterTypes.Boss)
            {
                bossHuntTimer += Time.deltaTime;
                if (Vector3.Distance(character.GetPlayerPosition(), character.GetPosition()) <= 2f || bossHuntTimer >= bossHuntTime)
                {
                    character.isAttacking = false;
                    character.isMoving = false;
                    GetComponent<BossStateMachineController>().reloadCountdown += 2;
                    Destroy(this);
                }
            }
        }
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathfindingSmoothing(waypoints, transform.position, character.turnDistance, 0);
            pathIndex = 0;
            pathfindingErrorCheck = 0;
            character.isMoving = true;
        }
        else if (pathfindingErrorCheck >= 10)
        {
            errorMovement = 1;
        }
        else
        {
            refreshTimer = 0;  // Try and find a new path
            pathfindingErrorCheck++;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null && path.turnBoundaries != null && debugPath)
            path.DrawWithGizmos();
    }
}
