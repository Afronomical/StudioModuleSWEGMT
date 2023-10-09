using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform Target; // Reference to the player's transform
    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

    void LateUpdate()
    {
        // Calculate the desired camera position based on the player's position and offset
        Vector3 desiredPosition = Target.position + Offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(Target);
    }
}
