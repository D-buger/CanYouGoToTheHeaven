using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Teleport()
    {
        if (!StageManager.PlayerInStage)
            StageManager.Instance.PlayerTeleportToStage();
        else
        {
            StageManager.Instance.PlayerTeleportToBonusRoom(transform.position);
            gameObject.SetActive(false);
        }
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
