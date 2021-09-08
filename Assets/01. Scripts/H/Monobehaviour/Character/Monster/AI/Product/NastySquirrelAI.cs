using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NastySquirrelAI : PatrolMonster
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDamage;
    [SerializeField] float velocity;
    bool isAttacking;
    [SerializeField] float prepareAttackDelay;
    WaitForSeconds waitPrepareAttackDelay;

    void Start()
    {
        OperateStart();
        waitPrepareAttackDelay = new WaitForSeconds(prepareAttackDelay);
    }

    void Update()
    {
        SearchPlayer();
        if (!detectPlayer)
        {
            if (isAttacking)
            {
                return;
            }
            CheckFront();
        }
        else
        {
            if (!isAttacking)
            {
                StartCoroutine(ThrowAcorn());
            }
        }
    }

    private void FixedUpdate()
    {
        if (!detectPlayer)
        {
            if (isAttacking)
            {
                return;
            }
            MoveForward();
        }
    }

    [System.Serializable]
    struct DebugOption
    {
        public bool showVisualRange;
    }
    [SerializeField] DebugOption debugOption;

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어 충돌 메소드를 플레이어 스크립트로 옮기기 바람");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }

    IEnumerator ThrowAcorn()
    {
        isAttacking = true;
        yield return waitPrepareAttackDelay;
        /*while (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)) //던지는 모션 종료?
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f) //던지는 모션이 거의 끝날 때 쯤
            {
                ShotProjectile(projectile, projectileDamage, 1, velocity, 0);
            }
            yield return waitForEndOfFrame;
        }*/
        ShotProjectile(projectile, projectileDamage, 1, velocity, 0);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (debugOption.showVisualRange)
        {
            Gizmos.DrawWireSphere(transform.position, visualRange);
            Gizmos.color = Color.cyan;
        }
    }
}
