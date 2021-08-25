using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float dampTime = 0.15f;
    private float velocity = 0;

    private Transform targetTransform;
    private Camera mainCamera;

    private Vector2 cameraSize;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraSize = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 point = mainCamera.WorldToViewportPoint(targetTransform.position);
        Vector3 delta = targetTransform.position -
            mainCamera.ViewportToWorldPoint(new Vector3(point.x, 0.5f, point.z));
        Vector3 destination = transform.position + delta;

        Vector3 value = new Vector3(transform.position.x,
            Mathf.SmoothDamp(transform.position.y, destination.y, ref velocity, dampTime)
            ,transform.position.z);
        transform.position = value;
    }
}
