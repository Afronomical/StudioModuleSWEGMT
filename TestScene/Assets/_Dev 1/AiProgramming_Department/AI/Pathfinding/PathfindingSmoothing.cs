/*
 *This script 
 *
 *Written by Aaron
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSmoothing
{
    public readonly Vector3[] lookPoints;
    public readonly PathfindingLine[] turnBoundaries;
    public readonly int finishLineIndex;

    public PathfindingSmoothing(Vector3[] waypoints, Vector3 startPos, float turnDist)
    {
        if (waypoints != null)
        {
            lookPoints = waypoints;
            turnBoundaries = new PathfindingLine[lookPoints.Length];
            finishLineIndex = turnBoundaries.Length - 1;  // Gets the index of the last waypoint

            Vector2 previousPoint = startPos;
            for (int i = 0; i < lookPoints.Length; i++)
            {
                Vector2 currentPoint = lookPoints[i];
                Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
                Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDist;
                turnBoundaries[i] = new PathfindingLine(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDist);
                previousPoint = turnBoundaryPoint;
            }
        }
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.white;
        foreach (PathfindingLine l in turnBoundaries)
            l.DrawWithGizmos(1);
    }
}
