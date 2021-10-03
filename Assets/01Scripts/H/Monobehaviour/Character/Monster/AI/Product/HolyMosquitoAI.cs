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
        //���⿡ ������ �������� ���� �޼ҵ带 �ۼ��ؾ���!
        Debug.LogWarning($"{gameObject.name}: ���� ���Ḧ ���� �޼ҵ带 �����ؾ���! �ּ� ����");
    }
}
