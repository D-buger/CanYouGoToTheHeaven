using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAlienAI : WalkMonster
{
    float attackDelay;
    [SerializeField] int attackPerSecond;
    float attackDuration;
    int projectileCount;
    float totalShotAngle;

    [SerializeField] GameObject laser;
    EnemyLaser laserComp;

    bool isAlreadyDetectPlayer;
    [SerializeField, Range(1f, 20f)] float height = 5;
    [SerializeField] float visualRange;
    bool isAttacking = false;

    Coroutine attackCoroutine = null;

    protected override void SettingVariables()
    {
        base.SettingVariables();
        StopAllCoroutines();
        attackDelay = StringToFloat(GetDataWithVariableName("AttackDelay"));
        attackDuration = StringToFloat(GetDataWithVariableName("AttackDuration"));
        projectileCount = (int)StringToFloat(GetDataWithVariableName("ProjectileCount"));
        totalShotAngle = StringToFloat(GetDataWithVariableName("TotalShotAngle"));
        visualRange = StringToFloat(GetDataWithVariableName("CognitiveRange"));
        isAlreadyDetectPlayer = false;
        isAttacking = false;
    }

    private void Awake()
    {
        SettingData();
        GameObject laserObj = Instantiate(laser);
        laserObj.transform.SetParent(transform);
        laserObj.transform.position = transform.position;
        laserComp = laserObj.GetComponent<EnemyLaser>();
        laserComp.Initialize(projectileCount, totalShotAngle);
        for (int i = 0; i < laserComp.laserCollider.Count; i++)
        {
            laserComp.laserCollider[i].rayDamage = StageManager.Instance.PlayerRoom <= 3 ? 1 : 2;
            laserComp.laserCollider[i].attackDelay = 1f / attackPerSecond;
        }
    }

    void Start()
    {
        OperateStart();
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        for (int i = 0; i < laserComp.laserComps.Count; i++)
        {
            laserComp.laserComps[i].gameObject.SetActive(false);
        }
        laserComp.particleComp.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isAlreadyDetectPlayer)
        {
            FindPlayer();
            return;
        }
        else
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        CheckFrontWall();
    }

    IEnumerator Attack()
    {
        WaitForSeconds atkdel = new WaitForSeconds(attackDelay);
        WaitForSeconds atkDur = new WaitForSeconds(attackDuration);
        laserComp.particleComp.Play();
        while (true)
        {
            laserComp.particleComp.gameObject.SetActive(true);
            laserComp.particleComp.Play();
            yield return atkdel; //공격 준비

            laserComp.particleComp.gameObject.SetActive(false);
            for (int i = 0; i < laserComp.laserComps.Count; i++)
            {
                laserComp.laserComps[i].gameObject.SetActive(true);
            }


            yield return atkDur; //공격 종료
            for (int i = 0; i < laserComp.laserComps.Count; i++)
            {
                laserComp.laserComps[i].gameObject.SetActive(false);
            }
        }
    }

    void FindPlayer()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= visualRange)
        {
            isAlreadyDetectPlayer = true;
        }
    }

    private void FixedUpdate()
    {
        if (isAlreadyDetectPlayer)
        {
            if (!isAttacking)
            {
                if (transform.position.y - player.transform.position.y >= height) //너무 높게 올라가면
                {
                    rb2d.MovePosition(rb2d.position + Vector2.right * moveDir * movementSpeed * Time.deltaTime);
                }
                else
                {
                    rb2d.MovePosition(new Vector2(rb2d.position.x + (moveDir * movementSpeed * Time.deltaTime), rb2d.position.y + 5 * Time.deltaTime));
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }
}
