using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    public int rayDamage = 2;
    public float attackDelay;
    float delayCounter = 0f;

    // Update is called once per frame
    void Update()
    {
        if (delayCounter >= float.Epsilon)
        {
            delayCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (delayCounter >= float.Epsilon)
        {
            return;
        }
        if (_collision.CompareTag("Player"))
        {
            StageManager.Instance.Stat.CurrentHp -= rayDamage;
            delayCounter = attackDelay;
        }
    }
}
