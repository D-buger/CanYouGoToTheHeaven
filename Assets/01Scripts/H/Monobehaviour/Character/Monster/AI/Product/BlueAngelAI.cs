using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAngelAI : PatrolMonster
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDamage;
    [SerializeField] float projectileVelocity;
    [SerializeField] int projectileCount;
    float projectileLifetime;
    [SerializeField] float totalAngle;
    bool isAttacking;
    Coroutine attackCoroutine = null;
    [SerializeField] float attackDelay;

    private void Awake()
    {
        OperateAwake();
        OperateOnEnable();
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    void Start()
    {
        OperateStart();
    }

    protected override void SettingVariables()
    {
        base.SettingVariables();
        projectileDamage = StageManager.Instance.PlayerRoom <= 9 ? 1 : 2;
        isAttacking = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        projectileVelocity = StringToFloat(GetDataWithVariableName("ProjectileVelocity"));
        projectileCount = (int)StringToFloat(GetDataWithVariableName("ProjectileCount"));
        totalAngle = StringToFloat(GetDataWithVariableName("TotalShotAngle"));
        attackDelay = StringToFloat(GetDataWithVariableName("AttackDelay"));
        projectileLifetime = StringToFloat(GetDataWithVariableName("ProjectileLifetime"));
    }

    void Update()
    {
        SearchPlayer();
        OperateUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActionAfterDeath();
        }
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

    IEnumerator Attack()
    {
        isAttacking = true;
        WaitForSeconds waitForAttackDelay = new WaitForSeconds(attackDelay);
        yield return waitForAttackDelay;
        ShotProjectile(projectile, projectileDamage, projectileCount, projectileVelocity, totalAngle, projectileLifetime);
        isAttacking = false;
        attackCoroutine = null;
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
