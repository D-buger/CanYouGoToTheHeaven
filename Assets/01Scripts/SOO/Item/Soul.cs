using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public int value = 1;
    public Vector2 initPosition;

    private void OnEnable()
    {
        transform.position = initPosition;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //TODO : �ڵ����� �÷��̾� ������ �����ϸ鼭 Ʈ���ſ� ���Բ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
        {
            StageManager.Instance.Stat.Soul += value;
        }
    }
}
