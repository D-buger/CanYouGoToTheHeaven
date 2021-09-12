using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NastySquirrelAI : PatrolMonster
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDamage;
    [SerializeField] float projectileVelocity;
    [SerializeField] int projectileCount = 1;
    [SerializeField] float totalAngle = 0f;
    bool isAttacking;
    Coroutine attackCoroutine = null;
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
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(ThrowAcorn());
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

    WaitForSeconds wait100MilliSeconds = new WaitForSeconds(0.1f);

    IEnumerator ThrowAcorn()
    {
        isAttacking = true;
        animator.SetBool("isPrepareAttack", true);
        yield return waitPrepareAttackDelay;
        animator.SetBool("isPrepareAttack", false);
        animator.SetBool("isDonePrepareAttack", true);
        yield return wait100MilliSeconds;
        animator.SetBool("isDonePrepareAttack", false);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.99f) //애니메이션이 완전히 끝났는가?
        {
            yield return waitForEndOfFrame;
        }
        ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, totalAngle); //발사
        animator.SetBool("attackIsEnd", true);
        yield return wait100MilliSeconds;
        animator.SetBool("attackIsEnd", false);
        attackCoroutine = null;
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
