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
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어 충돌 메소드를 플레이어 스크립트로 옮기기 바람");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }
}
