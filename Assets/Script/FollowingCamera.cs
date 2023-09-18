using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform backgroundLayer; // Specify the background layer object

    private Vector3 offset; // Offset distance between the camera and the background

    void Start()
    {
        // Calculate the initial offset between the camera and the background
        offset = transform.position - backgroundLayer.position;
    }

    void LateUpdate()
    {
        // Set the camera's position to the background's position plus the offset
        Vector3 targetPosition = backgroundLayer.position + offset;
        targetPosition.z = transform.position.z; // Keep the same Z position
        transform.position = targetPosition;
    }
}