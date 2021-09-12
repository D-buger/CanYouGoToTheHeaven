using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}: 플레이어와 접촉");
            Debug.LogWarning($"{gameObject.name}: 수정해야할 내용이 있음! 주석 참고");
            //해당 부분에 포탈을 탄 후 명령을 작성해야함
            Destroy(gameObject);
        }
    }
}
