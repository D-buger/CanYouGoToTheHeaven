using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}: �÷��̾�� ����");
            Debug.LogWarning($"{gameObject.name}: �����ؾ��� ������ ����! �ּ� ����");
            //�ش� �κп� ��Ż�� ź �� ����� �ۼ��ؾ���
            Destroy(gameObject);
        }
    }
}
