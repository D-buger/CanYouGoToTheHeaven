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
        OperateUpdate();
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
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }

    private void OnEnable()
    {
        OperateOnEnable();
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
