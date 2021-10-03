using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanktonAlienAI : HMonster
{
    [SerializeField, Tooltip("�÷��̾ ���� ���� �ش� �ð����� �ӹ� ��� ������� ����")] float inflictDamageDelay;
    WaitForSeconds waitInflictDmgDelay;
    [SerializeField] int damage = 2;
    bool playerInCursedPlatform = false;

    // Start is called before the first frame update

    private void OnEnable()
    {
        OperateOnEnable();
    }

    void Start()
    {
        OperateStart();
        waitInflictDmgDelay = new WaitForSeconds(inflictDamageDelay);
    }

    Coroutine contactCursedPlatformCoroutine = null;

    IEnumerator CursedDamage()
    {
        yield return waitInflictDmgDelay;

        Debug.LogWarning($"{gameObject.name}: �÷��̾�� ������� �ִ� ����� ���⿡ ������!!!");

        contactCursedPlatformCoroutine = null;
    }

    private void Update()
    {
        CheckPlayerInTheCursedPlatform();
    }

    void CheckPlayerInTheCursedPlatform()
    {
        if (playerInCursedPlatform) //�÷��̾ ������ �ִµ�
        {
            if (contactCursedPlatformCoroutine == null) //����� �ִ� �ڷ�ƾ�� null�̸�
            {
                contactCursedPlatformCoroutine = StartCoroutine(CursedDamage()); //����� �ִ� �ڷ�ƾ ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //�÷��̾ ������ ������
        {
            playerInCursedPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //�÷��̾ �������� ������
        {
            playerInCursedPlatform = false;
            if (contactCursedPlatformCoroutine != null)
            {
                StopCoroutine(contactCursedPlatformCoroutine);
                contactCursedPlatformCoroutine = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }
}
