using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }
    public Vector2 downLimit;
    public Vector2 horizontalLimit;

    public Transform followingTarget;

    public float smoothSpeed = 0.125f; 
    public Vector3 offset;

    public Camera mainCamera;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetFollowTarget(Transform character, float cameraSize = 5)
    {
        followingTarget = character;
        mainCamera.DOOrthoSize(cameraSize, 0.5f);
    }

    void LateUpdate()
    {
        if (followingTarget == null) 
        {
            return;
        }
        Vector3 desirePosition = followingTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, horizontalLimit.x, horizontalLimit.y);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, downLimit.x, downLimit.y);
        transform.position = smoothedPosition;
    }

    
}
