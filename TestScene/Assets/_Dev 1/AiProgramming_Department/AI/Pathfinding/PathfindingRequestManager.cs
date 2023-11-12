/*
 *This script creates a queue for AI characters requesting a path to be found
 *it prevents lots of pathfinding from happening at the same time
 *It should be placed on the AI Pathfinding object
 *
 *Written by Aaron
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PathfindingRequestManager : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();
    static PathfindingRequestManager instance;
    Pathfinding pathfinding;


    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }


    private void Update()
    {
        if (results.Count > 0)  // If there are results in the queue
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)  // Go through each item in the queue
                {
                    PathResult result = results.Dequeue();  // Remove it
                    if (result.callback != null)
                        result.callback(result.path, result.success);  // Callback to the state script
                }
            }
        }
    }


    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate  // Create a thread
        {
            instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }


    public void FinishedProcessingPath(PathResult result)
    {
        lock(results)  // Only add one to the queue at a time
            results.Enqueue(result);  // Add the pathfinding result to the queue
    }
}


public struct PathRequest  // The request for a path that is created by the state scripts
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public StateBaseClass state;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, StateBaseClass _state, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        state = _state;
        callback = _callback;
    }
}


public struct PathResult  // The result of the pathfinding that is created by Pathfinding.FindPath
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}