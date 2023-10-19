using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    PathfindingGrid grid;

    public Transform seeker, target;


    private void Awake()
    {
        grid = GetComponent<PathfindingGrid>();
    }

    private void Update()
    {
        if (grid.GetNodeFromPosition(target.transform.position).walkable)
            FindPath(seeker.position, target.position);
    }


    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        PathfindingNode startNode = grid.GetNodeFromPosition(startPos);
        PathfindingNode targetNode = grid.GetNodeFromPosition(targetPos);

        List<PathfindingNode> openNodes = new List<PathfindingNode>();
        HashSet<PathfindingNode> closedNodes = new HashSet<PathfindingNode>();
        openNodes.Add(startNode);

        while (openNodes.Count > 0)
        {
            PathfindingNode currentNode = openNodes[0];
            for (int i = 1; i < openNodes.Count; i++)  // Get the open node with the lowest fCost
                if (openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost && openNodes[i].hCost < currentNode.hCost)
                    currentNode = openNodes[i];

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);  // Close the node with the lowest fCost

            if (currentNode == targetNode)  // If the path has reached it's destination
            {
                RetracePath(startNode, targetNode);
                return;  // Leave the loop
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
                }
            }
        }
    }


    private void RetracePath(PathfindingNode startNode, PathfindingNode endNode)  // Get a list of nodes for the path
    {
        List<PathfindingNode> path = new List<PathfindingNode>();
        PathfindingNode currentNode = endNode;

        while (currentNode != startNode)  // Repeat until we reach the start
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;  // Move down the path
        }

        path.Reverse();  // This list is backwards so we have to reverse it

        grid.path = path;
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
