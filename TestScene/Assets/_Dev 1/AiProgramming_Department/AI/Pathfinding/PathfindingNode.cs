using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public bool walkable;
    public Vector2 worldPosition;

    public PathfindingNode(bool _walkable, Vector2 _worldPosition)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
    }
}
