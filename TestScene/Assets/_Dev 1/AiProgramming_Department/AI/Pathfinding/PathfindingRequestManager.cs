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

public class PathfindingRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    static PathfindingRequestManager instance;
    Pathfinding pathfinding;
    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);  // Create a new request
        instance.pathRequestQueue.Enqueue(newRequest);  // Add this request to the queue
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)  // If there is something in the queue and we aren't processing something
        {
            currentPathRequest = pathRequestQueue.Dequeue();  // Get and remove the first item from the queue
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);  // Return the result
        isProcessingPath = false;
        TryProcessNext();  // Try to move onto the next request
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
