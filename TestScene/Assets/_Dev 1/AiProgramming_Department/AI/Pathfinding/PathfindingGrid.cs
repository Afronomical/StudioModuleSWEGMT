/*
 *This script should be placed on an AI Pathfinding object at position 0, 0, 0 in the world
 *It creates a grid of nodes that covers the map, each node will figure out if it is walkable or not
 *
 *Written by Aaron
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    public Transform player;
    public Vector2 gridWorldSize;  // How large does the grid need to be to cover the map
    public float nodeRadius;  // How big should each node be?
    public LayerMask unwalkableLayer;
    public bool debugGizmos;  // Will show all of the nodes
    
    private PathfindingNode[,] grid;  // A 2D array holding all of the nodes in the grid
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;


    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);  // How many nodes should be in the grid?
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }


    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    private void CreateGrid()
    {
        grid = new PathfindingNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;  // Find position of the bottom left node

        for (int x = 0; x < gridSizeX; x++)  // For every node needed for the grid
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);  // Find the position of the node
                worldPoint.z = 0;
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableLayer));  // Check if the node is touching an obstacle or not
                grid[x, y] = new PathfindingNode(walkable, worldPoint, x, y);  // Create the node and add it to the array
            }
        }

        GetComponent<Pathfinding>().openNodes = new PathfindingHeap<PathfindingNode>(MaxSize);
    }


    public List<PathfindingNode> GetNeighbours(PathfindingNode node)
    {
        List<PathfindingNode> neighbours = new List<PathfindingNode>();

        for (int x = -1; x <= 1; x++)  // Check in a 3x3 square around this node
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)  // If it is this node
                    continue;  // Skip this iteration

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeX)  // If it is inside the grid
                    neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }

    public PathfindingNode GetNodeFromPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;  // How far across the grid this point is
        float percentY = (worldPosition.y - transform.position.x + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);  // Make sure the value is on the grid
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }


    public List<PathfindingNode> path;
    private void OnDrawGizmos()
    {
        if (debugGizmos)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));  // Draw an outline of the grid

            if (grid != null)
            {
                PathfindingNode playerNode = GetNodeFromPosition(player.transform.position);

                foreach (PathfindingNode n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;  // Set the node to white if walkable or red if not
                    if (playerNode == n)
                        Gizmos.color = Color.cyan;  // Set the node to white if the player is on it
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.black;

                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));  // Draw each node
                }
            }
        }
    }
}
