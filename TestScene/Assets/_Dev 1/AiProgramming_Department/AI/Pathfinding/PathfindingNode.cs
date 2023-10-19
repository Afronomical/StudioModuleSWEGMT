using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public bool walkable;
    public Vector2 worldPosition;
    public int gridX, gridY;

    public int gCost;  // Distance from start
    public int hCost;  // Distance from target
    public PathfindingNode parent;

    public PathfindingNode(bool _walkable, Vector2 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost  // Overall cost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
