using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 playerMoveTo;

    private void Teleport()
    {
        StageManager.Instance.Player.transform.position = playerMoveTo;
        StageManager.Instance.PlayerTeleportToStage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
            GameManager.Instance.input.activeCallback += Teleport;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
            GameManager.Instance.input.activeCallback -= Teleport;
    }
}
