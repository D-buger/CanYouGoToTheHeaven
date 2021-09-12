using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTransform;
    private Camera mainCamera;

    float velocity = 0;

    [SerializeField]
    private float smoothTime = 0.5f;

    [SerializeField]
    private float offsetY;

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
    }

    private void CheckTargetPos()
    {
        CheckChanged();
        Vector3 targetPosToViewp = 
            mainCamera.WorldToViewportPoint(targetTransform.position);
        if (targetPosToViewp.y > topY)
        {
            transform.position += changedValue;
        }
        else if(targetPosToViewp.y < bottomY)
        {
            transform.position += changedValue;
        }
        else
        {
            CamMoveSmooth();
        }

    }

    private void CheckChanged()
    {
        changedValue.y = targetTransform.position.y - previousPos.y;
        previousPos.y = targetTransform.position.y;
    }

    private void CamMoveSmooth()
    {
        //lerp 대신 smoothdamp? => lerp보다 더 부드러움을 제공
        Vector3 desiredPos = targetTransform.position;
        Vector3 smoothedPosition = transform.position;
        smoothedPosition.y = Mathf.SmoothDamp(
            transform.position.y, desiredPos.y + offsetY, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}
