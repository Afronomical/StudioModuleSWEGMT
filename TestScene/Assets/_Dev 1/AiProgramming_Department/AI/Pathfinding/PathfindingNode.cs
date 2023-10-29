/*
 *This class is used by PathfindingGrid
 *
 *Written by Aaron
 */

using System.Collections;
using UnityEngine;

public class PathfindingNode : IHeapItem<PathfindingNode>
{
    public bool walkable;
    public Vector2 worldPosition;
    public int gridX, gridY;

    public int gCost;  // Distance from start
    public int hCost;  // Distance from target
    public PathfindingNode parent;
    int heapIndex;

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

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(PathfindingNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)  // If the fCost is the same
            compare = hCost.CompareTo(nodeToCompare.hCost);  // Compare the hCost
        return -compare;
    }
}
