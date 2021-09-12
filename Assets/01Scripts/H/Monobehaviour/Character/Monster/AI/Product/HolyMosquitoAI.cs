using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMosquitoAI : HMonster
{
    private void Start()
    {
        OperateStart();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            StealingJuice();
        }
    }

    void StealingJuice()
    {
        //���⿡ ������ �������� ���� �޼ҵ带 �ۼ��ؾ���!
        Debug.LogWarning($"{gameObject.name}: ���� ���Ḧ ���� �޼ҵ带 �����ؾ���! �ּ� ����");
    }
}
