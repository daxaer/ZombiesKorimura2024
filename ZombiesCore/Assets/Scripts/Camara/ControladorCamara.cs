using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    [HideInInspector]
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.3f;   // Smoothness of camera movement
    public Vector3 offset;               // Offset of the camera from the player
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
       if (player == null) { return; }
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity,smoothSpeed);
    }
}
