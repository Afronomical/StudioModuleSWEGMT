/*
 *This script should be placed on the AI Pathfinding object
 *It uses the grid of nodes that has been created to find the shortest
 *path to the destination
 *
 *Written by Aaron
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    PathfindingGrid grid;
    PathfindingRequestManager requestManager;
    [HideInInspector] public PathfindingHeap<PathfindingNode> openNodes;
    private HashSet<PathfindingNode> closedNodes = new HashSet<PathfindingNode>();
    public bool debugTimeTaken;


    private void Awake()
    {
        grid = GetComponent<PathfindingGrid>();
        requestManager = GetComponent<PathfindingRequestManager>();
    }


    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }


    public IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        PathfindingNode startNode = grid.GetNodeFromPosition(startPos);
        PathfindingNode targetNode = grid.GetNodeFromPosition(targetPos);

        if (targetNode.walkable)
        {
            openNodes.Clear();  // Doing this rather than creating a new heap prevents garbage from being created
            closedNodes.Clear();
            openNodes.Add(startNode);

            while (openNodes.Count > 0)
            {
                PathfindingNode currentNode = openNodes.RemoveFirst();
                closedNodes.Add(currentNode);  // Close the node with the lowest fCost

                if (currentNode == targetNode)  // If the path has reached it's destination
                {
                    sw.Stop();
                    if (debugTimeTaken)
                        print("AI Path Found: " + sw.ElapsedMilliseconds + " milliseconds");

                    pathSuccess = true;
                    break;  // Leave the loop
                }

                foreach (PathfindingNode neighbour in grid.GetNeighbours(currentNode))  // For all nodes around the current one
                {
                    if (!neighbour.walkable || closedNodes.Contains(neighbour))  // If this node is not walkable or has been checked
                        continue;  // Skip this iteration

                    int newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbour);  // Get the gCost of this neighbour
                    if (newMovementCost < neighbour.gCost || !openNodes.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openNodes.Contains(neighbour))
                            openNodes.Add(neighbour);
                        else
                            openNodes.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;

        if (pathSuccess)  // If it found a path
            waypoints = RetracePath(startNode, targetNode);  // Create an array of waypoints using the path found
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);  // Return the result
    }


    private Vector3[] RetracePath(PathfindingNode startNode, PathfindingNode endNode)  // Get a list of nodes for the path
    {
        List<PathfindingNode> path = new List<PathfindingNode>();
        PathfindingNode currentNode = endNode;

        while (currentNode != startNode)  // Repeat until we reach the start
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;  // Move down the path
        }

        Vector3[] waypoints = SimplifyPath(path, startNode);  // Create an array of waypoint positions
        Array.Reverse(waypoints);  // This list is backwards so we have to reverse it
        return waypoints;
    }


    private Vector3[] SimplifyPath(List<PathfindingNode> path, PathfindingNode startNode)  // Removes useless waypoints in a straight line
    {
        if (path.Count < 1)
            return new Vector3[0];

        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        waypoints.Add(path[0].worldPosition);  // Add in the last position

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);  // Get the direction between two waypoints
            if (directionNew != directionOld)  // If it is pointing in a different direction
                waypoints.Add(path[i].worldPosition);  // Add the waypoint to a list
            
            directionOld = directionNew;

            if (i == path.Count - 1 && directionOld != new Vector2(path[i].gridX, path[i].gridY) - new Vector2(startNode.gridX, startNode.gridY))
                waypoints.Add(path[path.Count - 1].worldPosition);
        }
        return waypoints.ToArray();
    }


    private int GetDistance(PathfindingNode nodeA, PathfindingNode nodeB)  // Find the distance between two nodes
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
