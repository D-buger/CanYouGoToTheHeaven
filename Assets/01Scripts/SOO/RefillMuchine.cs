using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillMuchine : MonoBehaviour
{
    private bool isWork = true;

    private void OnEnable()
    {
        isWork = true;
    }

    private void Refill()
    {
        if (isWork) {
            StageManager.Instance.Stat.watergun.Model.WaterAmount =
                 StageManager.Instance.Stat.watergun.Model.MaxWaterAmount;
            StageManager.data.RefillCount++;
            isWork = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
        {
            GameManager.Instance.input.activeCallback += Refill;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
        {
            GameManager.Instance.input.activeCallback -= Refill;
        }
    }
}
