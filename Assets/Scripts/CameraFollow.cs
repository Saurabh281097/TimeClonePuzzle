using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Player
    public float smoothSpeed = 5f;    // Follow smoothness
    public Vector3 offset;            // Camera offset
    public float lookAheadFactor = 2f; // Look ahead distance

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastTargetPosition;

    void Start()
    {
        if (target != null)
            lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate movement direction
        Vector3 delta = target.position - lastTargetPosition;

        // Look ahead in movement direction
        Vector3 lookAhead = delta.normalized * lookAheadFactor;

        // Final desired position
        Vector3 desiredPosition = target.position + offset + lookAhead;

        // Smooth movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / smoothSpeed
        );

        lastTargetPosition = target.position;
    }
}