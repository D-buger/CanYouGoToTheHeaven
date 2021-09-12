using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPosition;
    [SerializeField, Space(10)]
    private bool cameraLock = false;
    [SerializeField]
    private Transform cameraLockPosition;

    private Transform playerTransform;
    private GameObject camera;
    private CameraManager cameraManager;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        camera = Camera.main.gameObject;
        cameraManager = camera.GetComponent<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraLockPosition.gameObject.SetActive(false);
        cameraLockPosition.gameObject.SetActive(true);
        cameraManager.CameraLock = cameraLock;
        playerTransform.position = teleportPosition.position;
        camera.transform.position = cameraLockPosition.position;
        
    }
}
