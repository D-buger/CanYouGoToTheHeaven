using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMosquitoAI : HMonster
{
    private void Start()
    {
        OperateStart();
    }

    protected override void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        base.OperateOnCollisionEnter2D(_collision);
        StealingJuice();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }

    void StealingJuice()
    {
        //여기에 음료의 일정량을 뺏는 메소드를 작성해야함!
        Debug.LogWarning($"{gameObject.name}: 남은 음료를 뺏는 메소드를 구현해야함! 주석 참고");
    }
}
