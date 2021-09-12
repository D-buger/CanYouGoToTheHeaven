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

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            Debug.LogWarning($"{gameObject.name}: �÷��̾� �浹 �޼ҵ带 �÷��̾� ��ũ��Ʈ�� �ű�� �ٶ�");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }

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
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.99f) //(������ �߻��ϴ�)�ִϸ��̼��� ������ �����°�?
        {
            yield return waitForEndOfFrame;
        }
        ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, totalAngle); //�߻�
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