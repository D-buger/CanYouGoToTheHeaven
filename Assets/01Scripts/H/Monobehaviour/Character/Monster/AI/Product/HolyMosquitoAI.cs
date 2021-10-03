using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMosquitoAI : HMonster
{
    private void Start()
    {
        OperateStart();
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    private void Update()
    {
        CheckDistanceFromPlayer();
    }

    protected override void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        StealingJuice();
        base.OperateOnCollisionEnter2D(_collision);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }

    void StealingJuice()
    {
        StageManager.Instance.Stat.watergun.Model.WaterAmount -= (int)(StageManager.Instance.Stat.watergun.Model.MaxWaterAmount * 0.1f);
    }
}
