using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StageManager.Instance.PlayerTeleportToStage();
    }
}
