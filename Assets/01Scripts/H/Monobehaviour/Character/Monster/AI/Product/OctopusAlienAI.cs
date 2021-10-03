using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusAlienAI : PatrolMonster
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
            CheckFrontWall();
        }
        else
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
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

    WaitForSeconds wait100MilliSeconds = new WaitForSeconds(0.1f);

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isPrepareAttack", true);
        yield return waitPrepareAttackDelay;
        animator.SetBool("isPrepareAttack", false);
        animator.SetBool("isDonePrepareAttack", true);
        yield return wait100MilliSeconds;
        animator.SetBool("isDonePrepareAttack", false);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.99f) //(공격을 발사하는)애니메이션이 완전히 끝났는가?
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

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }
}
