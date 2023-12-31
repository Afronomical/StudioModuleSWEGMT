/*
 *This script creates a smooth path for the AI to follow through the waypoints
 *
 *Written by Aaron
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSmoothing
{
    public Vector3[] lookPoints;
    public PathfindingLine[] turnBoundaries;
    public int finishLineIndex;
    public int slowDownIndex;


    public PathfindingSmoothing(Vector3[] waypoints, Vector3 startPos, float turnDist, float stoppingDistance)
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

            float distFromEnd = 0;
            for (int i = lookPoints.Length - 1; i > 0; i--)
            {
                distFromEnd += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
                if (distFromEnd > stoppingDistance)
                {
                    slowDownIndex = i;
                    break;
                }
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
