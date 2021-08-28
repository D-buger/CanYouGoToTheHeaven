using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTransform;
    private Camera mainCamera;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float camSpeed = 1;

    [SerializeField, Range(0, 1)]
    private float topY;
    [SerializeField, Range(0, 1)]
    private float bottomY;

    private Vector3 previousPos;
    private Vector3 changedValue;

    private void Awake()
    {
        mainCamera = Camera.main;
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        previousPos = targetTransform.position;
    }

    private void LateUpdate()
    {
        CheckTargetPos();
        CamMoveSmooth();
    }

    private void CheckChanged()
    {
        changedValue = targetTransform.position - previousPos;
        previousPos = targetTransform.position;
    }

    private void CheckTargetPos()
    {
        CheckChanged();
        Vector3 targetPosToViewp = mainCamera.WorldToViewportPoint(targetTransform.position);
        if (targetPosToViewp.y > topY)
        {
            camSpeed = 0;
        }
        else if(targetPosToViewp.y < bottomY)
        {
            camSpeed = 0;
        }
        else
        {
            camSpeed = 0.1f;
        }

    }

    private void CamMoveSmooth()
    {
        Vector3 desiredPos = targetTransform.position + offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, desiredPos, camSpeed);
        transform.position = lerpPosition;
    }
}
