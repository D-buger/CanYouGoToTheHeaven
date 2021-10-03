using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanktonAlienAI : HMonster
{
    [SerializeField, Tooltip("플레이어가 범위 내에 해당 시간동안 머물 경우 대미지를 가함")] float inflictDamageDelay;
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

        Debug.LogWarning($"{gameObject.name}: 플레이어에게 대미지를 주는 방식을 여기에 넣을것!!!");

        contactCursedPlatformCoroutine = null;
    }

    private void Update()
    {
        CheckPlayerInTheCursedPlatform();
    }

    void CheckPlayerInTheCursedPlatform()
    {
        if (playerInCursedPlatform) //플레이어가 범위에 있는데
        {
            if (contactCursedPlatformCoroutine == null) //대미지 주는 코루틴이 null이면
            {
                contactCursedPlatformCoroutine = StartCoroutine(CursedDamage()); //대미지 주는 코루틴 실행
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //플레이어가 범위에 들어오면
        {
            playerInCursedPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //플레이어가 범위에서 나가면
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
