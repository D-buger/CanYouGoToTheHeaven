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

    private void Awake()
    {
        mainCamera = Camera.main;
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        CheckTargetPos();
        CamMoveSmooth();
    }

    private void CheckTargetPos()
    {
        Vector3 targetPosToViewp = mainCamera.WorldToViewportPoint(targetTransform.position);
        if (targetPosToViewp.y > topY)
        {

            //offset.y = mainCamera.ViewportToWorldPoint();
        }
        else if(targetPosToViewp.y < bottomY)
        {

        }
        else
        {
        }

    }
    
    private void CamMoveSmooth()
    {
        Vector3 desiredPos = targetTransform.position + offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, desiredPos, camSpeed);
        transform.position = lerpPosition;
    }
}
