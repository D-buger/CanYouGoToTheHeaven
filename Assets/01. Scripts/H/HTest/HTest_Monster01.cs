using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_Monster01 : HTest_Monster
{
    bool detectPlayer;
    bool isTrackingPlayer;
    [SerializeField] float interval;
    [SerializeField] bool showDetectDistance;
    [SerializeField] bool showShotAngle;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shotAngle;
    [SerializeField] float projectileVelocity;
    [SerializeField] int projectileCount;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        detectPlayer = DetectPlayer();

        if (!isTrackingPlayer) //현재 문제: 코루틴이 이미 실행중일 때 플레이어가 감지 범위를 _나갔다가 들어와도_ 코루틴이 그 이전의 것이 반영됨
        {
            //플레이어가 감지범위를 나간 순간에 코루틴을 실행시키면 될 것. 또한 감지범위에 들어올 때 코루틴을 중지시키면 될것

            if (detectPlayer)
            {
                SetMovementBehavior(new HTest_IMovementBehavior_TrackingTarget());
                isTrackingPlayer = true;
            }
        }
        else
        {
            if (!detectPlayer)
            {
                StartCoroutine(Re_CheckingPlayer(interval));
            }
            else
            {
                StopCoroutine("Re_CheckingPlayer");
            }
        }
        currentMovement.OperateUpdate(rb2d, playersss, movementSpeed);
    }

    protected void FireProjectile(GameObject _projectile, float _shotAngle, float _velocity, int _projectileCount) //원거리 공격을 하는 적 및 아닌 적으로 클래스를 분리해서 넣을것
    {

    }

    IEnumerator Re_CheckingPlayer(float _interval)
    {
        WaitForSeconds delay = new WaitForSeconds(_interval);
        yield return delay;

        detectPlayer = DetectPlayer();

        if (detectPlayer)
        {
            StartCoroutine(Re_CheckingPlayer(interval));
        }
        else
        {
            SetMovementBehavior(new HTest_IMovementBehavior_Static());
            isTrackingPlayer = false;
            detectPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (showDetectDistance)
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawWireSphere(transform.position, detectDistance);
        }
        if (showShotAngle)
        {
            Gizmos.color = new Color(0, 0, 1);
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + 4, transform.position.y));
        }
    }
}
