using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector2 downLimit;
    public Vector2 horizontalLimit;


    public float smoothSpeed = 0.125f; 
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, horizontalLimit.x, horizontalLimit.y);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, downLimit.x, downLimit.y);
        transform.position = smoothedPosition;
    }
}
