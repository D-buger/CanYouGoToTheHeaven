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
    float projectileLifetime;
    bool isAttacking;
    Coroutine attackCoroutine = null;
    [SerializeField] float prepareAttackDelay;

    void Start()
    {
        OperateStart();
    }

    protected override void SettingVariables()
    {
        base.SettingVariables();
        projectileDamage = StageManager.Instance.PlayerRoom <= 3 ? 1 : 2;
        projectileVelocity = StringToFloat(GetDataWithVariableName("ProjectileVelocity"));
        projectileCount = (int)StringToFloat(GetDataWithVariableName("ProjectileCount"));
        totalAngle = StringToFloat(GetDataWithVariableName("TotalShotAngle"));
        prepareAttackDelay = StringToFloat(GetDataWithVariableName("AttackDelay"));
        projectileLifetime = StringToFloat(GetDataWithVariableName("ProjectileLifetime"));
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    private void Awake()
    {
        OperateAwake();
    }

    void Update()
    {
        SearchPlayer();
        CheckDistanceFromPlayer();
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

    WaitForSeconds wait100MilliSeconds = new WaitForSeconds(0.1f);

    IEnumerator ThrowAcorn()
    {
        WaitForSeconds waitPrepareAttackDelay = new WaitForSeconds(prepareAttackDelay);
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
        ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, totalAngle, projectileLifetime); //발사
        animator.SetBool("attackIsEnd", true);
        yield return wait100MilliSeconds;
        animator.SetBool("attackIsEnd", false);
        attackCoroutine = null;
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
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
