using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform targetTransform;
    Camera mainCamera;

    [SerializeField]
    float topCameraLine, bottomCameraLine;

    Vector2 cameraSize;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraSize = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 LeftPointVec =
            new Vector2(transform.position.x - cameraSize.x / 2, transform.position.y + topCameraLine);
        Vector2 RightPointVec =
            new Vector2(transform.position.x + cameraSize.x / 2, transform.position.y + topCameraLine);
        Gizmos.DrawLine(LeftPointVec, RightPointVec);
        Gizmos.color = Color.white;
    }
#endif
}
