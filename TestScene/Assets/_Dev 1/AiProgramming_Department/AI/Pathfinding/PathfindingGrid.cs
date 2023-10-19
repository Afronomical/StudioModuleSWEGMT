using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    public Transform player;
    public Vector2 gridWorldSize;  // How large does the grid need to be to cover the map
    public float nodeRadius;  // How big should each node be?
    public LayerMask unwalkableLayer;
    
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
                grid[x, y] = new PathfindingNode(walkable, worldPoint);  // Create the node and add it to the array
            }
        }
    }


    public PathfindingNode GetNodeFromPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;  // How far across the grid this point is
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);  // Make sure the value is on the grid
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }


    private void OnDrawGizmos()
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

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));  // Draw each node
            }
        }
    }
}
