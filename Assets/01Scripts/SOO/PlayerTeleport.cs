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
    private GameObject Camera;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTransform.position = teleportPosition.position;
        
    }
}
