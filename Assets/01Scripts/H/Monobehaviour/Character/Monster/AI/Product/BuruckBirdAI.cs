using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuruckBirdAI : PatrolMonster
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDamage;
    [SerializeField] int projectileCount;
    [SerializeField] float projectileVelocity;
    [SerializeField] float shotAngle;
    [SerializeField] float prepareAttackDelay;
    WaitForSeconds waitPrepareAttackDelay;
    bool isAttacking;

    void Start()
    {
        OperateStart();
        waitPrepareAttackDelay = new WaitForSeconds(prepareAttackDelay);
    }

    private void Update()
    {
        SearchPlayer();
        if (detectPlayer)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return waitPrepareAttackDelay;
        /*while (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)) //던지는 모션 종료?
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f) //던지는 모션이 거의 끝날 때 쯤
            {
                ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, shotAngle);
            }
            yield return waitForEndOfFrame;
        }*/
        ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, shotAngle);
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }

    [System.Serializable]
    struct DebugOption
    {
        public bool showVisualRange;
    }
    [SerializeField] DebugOption debugOption;

    private void OnDrawGizmos()
    {
        if (debugOption.showVisualRange)
        {
            Gizmos.DrawWireSphere(transform.position, visualRange);
            Gizmos.color = Color.cyan;
        }
    }
}
