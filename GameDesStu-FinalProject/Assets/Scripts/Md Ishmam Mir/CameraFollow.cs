using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Usually the player
    public Vector3 offset = new Vector3(0, 0, -10); // Keep camera behind in 2D
    public float smoothSpeed = 0.125f;

    [Header("Camera Bounds")]
    public Vector2 horizontalLimit = new Vector2(-10f, 10f); // Min X, Max X
    public Vector2 verticalLimit = new Vector2(-5f, 5f);     // Min Y, Max Y

    void LateUpdate()
    {
        if (target == null) return;

        // Desired and smoothed camera positions
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp within limits
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, horizontalLimit.x, horizontalLimit.y);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, verticalLimit.x, verticalLimit.y);

        transform.position = smoothedPosition;
    }
}
