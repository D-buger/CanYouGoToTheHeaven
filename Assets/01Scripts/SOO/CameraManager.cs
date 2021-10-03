using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTransform;
    private Camera mainCamera;

    private float velocity = 0;

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

    private bool cameraLock = false;
    public bool CameraLock
    {
        get => cameraLock;
        set
        {
            cameraLock = value;
            previousPos = targetTransform.position;
        }
    }

    private Vector3 World2Viewport(Vector3 vec) => mainCamera.WorldToViewportPoint(vec);
    public Vector3 Screen2World(Vector3 vec) => mainCamera.ScreenToWorldPoint(vec);

    private void Awake()
    {
        mainCamera = Camera.main;

        targetTransform = StageManager.Instance.Player.transform;
        previousPos = targetTransform.position;
    }

    private void LateUpdate()
    {
        if(!CameraLock)
            CheckTargetPos();
    }

    public bool TargetIsInRange()
    {
        Vector3 targetPosToViewp =
            World2Viewport(targetTransform.position);
        if (targetPosToViewp.y <= topY && targetPosToViewp.y >= bottomY)
        {
            return true;
        }
        return false;
    }

    private void CheckTargetPos()
    {
        CheckChanged();
        Vector3 targetPosToViewp =
            World2Viewport(targetTransform.position);

        if (targetPosToViewp.y > topY)
        {
            transform.position += changedValue;
        }
        else if (targetPosToViewp.y < bottomY)
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

    public void CamPositionChange(Vector2 position) 
        => transform.position = new Vector3(position.x, position.y, transform.position.z); 

    public void CamPositionChangeY(float y)
        => transform.position = new Vector3(transform.position.x, y, transform.position.z);
}
