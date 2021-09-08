using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySquirrelAI : PatrolMonster
{
    Coroutine dashCoroutine;
    bool isDashing;
    [SerializeField, Range(0f, 8f), Tooltip("돌진 준비 시간(차징)")] float timeToPrepareDash;
    bool prepareDash;
    [SerializeField, Range(0f, 20f), Tooltip("돌진의 속도")] float dashingSpeed;
    WaitForSeconds waitPrepareDashDelay;
    [SerializeField, Range(0f, 3f), Tooltip("돌진 후 딜레이. 해당 상태동안 아무런 행동도 할 수 없다.")] float dashAfterCooldown;
    WaitForSeconds waitDashCooldownDelay;
    [SerializeField, Range(0f, 8f), Tooltip("돌진 지속시간(돌진 속도와 함께 돌진의 거리를 결정한다)")] float dashingDuration;
    WaitForSeconds waitDashingDurationDelay;

    WaitForSeconds waitFor1Seconds = new WaitForSeconds(1f);



    [System.Serializable]
    struct DebugOption
    {
        public bool showVisualRange;
        public bool showDashDistance;
    }
    [SerializeField] DebugOption debugOption;

    // Start is called before the first frame update
    void Start()
    {
        OperateStart();
        animator = GetComponent<Animator>();
        waitPrepareDashDelay = new WaitForSeconds(timeToPrepareDash);
        waitDashCooldownDelay = new WaitForSeconds(dashAfterCooldown);
        waitDashingDurationDelay = new WaitForSeconds(dashingDuration);
    }

    IEnumerator Dash()
    {
        Vector2 destination;
        while (detectPlayer)
        {
            Debug.Log("돌진 준비");
            prepareDash = true;
            yield return waitPrepareDashDelay;
            destination = FindDirectionVector(player.transform.position);
            Debug.Log($"목표 지점: {destination}");
            Debug.Log("돌진");
            prepareDash = false;
            isDashing = true;
            rb2d.velocity = destination * dashingSpeed;
            yield return waitDashingDurationDelay;
            Debug.Log("돌진 종료");
            rb2d.velocity = Vector2.zero;
            isDashing = false;
            yield return waitDashCooldownDelay;
            Debug.Log("돌진 대기시간 끝");
        }
        dashCoroutine = null;
    }

    private void FixedUpdate()
    {
        if (dashCoroutine == null)
        {
            MoveForward();
        }
    }

    private void Update()
    {
        SearchPlayer();
        if (dashCoroutine == null)
        {
            if (detectPlayer == true)
            {
                dashCoroutine = StartCoroutine(Dash());
                return;
            }
            CheckFrontWall();
        }
    }

    private void OnDrawGizmos()
    {
        if (debugOption.showVisualRange)
        {
            Gizmos.DrawWireSphere(transform.position, visualRange);
            Gizmos.color = Color.cyan;
        }
        if (debugOption.showDashDistance)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + dashingSpeed * dashingDuration, transform.position.y + dashingSpeed * dashingDuration));
            Gizmos.color = Color.blue;
        }
    }
}
