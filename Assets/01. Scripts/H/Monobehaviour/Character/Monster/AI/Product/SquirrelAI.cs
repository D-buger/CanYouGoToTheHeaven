using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : Patrol
{
    void Start()
    {
        OperateStart();
    }

    void Update()
    {
        OperateUpdate();
    }

    private void FixedUpdate()
    {
        OperateFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            Debug.LogWarning($"{gameObject.name}: �÷��̾� �浹 �޼ҵ带 �÷��̾� ��ũ��Ʈ�� �ű�� �ٶ�");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }
}
