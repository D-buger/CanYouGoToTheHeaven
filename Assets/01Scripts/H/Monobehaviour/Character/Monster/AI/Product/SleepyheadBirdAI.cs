using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyheadBirdAI : PatrolMonster
{
    Coroutine dashCoroutine;
    bool isDashing;
    [SerializeField, Range(0.4f, 5f), Tooltip("최초로 플레이어를 발견한 후 잠에서 깰때 까지의 시간")] float wakeUpFromSleep;
    WaitForSeconds waitWakeUpFromSleep;
    Coroutine wakeUpCoroutine = null;
    bool isAlreadyAwake = false;
    [SerializeField, Range(0f, 8f), Tooltip("돌진 준비 시간(차징)")] float timeToPrepareDash;
    bool prepareDash;
    [SerializeField, Range(0f, 20f), Tooltip("돌진의 속도")] float dashingSpeed;
    WaitForSeconds waitPrepareDashDelay;
    [SerializeField, Range(0f, 3f), Tooltip("돌진 후 딜레이. 해당 상태동안 아무런 행동도 할 수 없다.")] float dashAfterCooldown;
    WaitForSeconds waitDashCooldownDelay;
    [SerializeField, Range(0f, 8f), Tooltip("돌진 지속시간(돌진 속도와 함께 돌진의 거리를 결정한다)")] float dashingDuration;
    WaitForSeconds waitDashingDurationDelay;

    Sprite defaultSprite;
    [SerializeField, Tooltip("인지범위 * 1.8f의 범위에 들어왔을때의 표정(조금 깸, 인지 못함)")] Sprite awakeFromDeepSleep;
    [SerializeField, Tooltip("인지범위에 최초로 들어와 잠이 완전히 깼을 때의 표정(이후 공격)")] Sprite firstIncounterForm;

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
        animator.enabled = false;
        waitPrepareDashDelay = new WaitForSeconds(timeToPrepareDash);
        waitWakeUpFromSleep = new WaitForSeconds(wakeUpFromSleep);
        waitDashCooldownDelay = new WaitForSeconds(dashAfterCooldown);
        waitDashingDurationDelay = new WaitForSeconds(dashingDuration);
        defaultSprite = sprite.sprite;
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

    void UpdateSpriteState()
    {
        if (isAlreadyAwake)
        {
            return;
        }
        if (wakeUpCoroutine != null)
        {
            sprite.sprite = firstIncounterForm;
            return;
        }
        else if(Vector2.Distance(player.transform.position, transform.position) <= visualRange)
        {
            sprite.sprite = firstIncounterForm;
        }
        else if (Vector2.Distance(player.transform.position, transform.position) <= visualRange * 1.8f)
        {
            sprite.sprite = awakeFromDeepSleep;
        }
        else
        {
            sprite.sprite = defaultSprite;
        }
    }

    IEnumerator WakeUpFromSleep()
    {
        sprite.sprite = firstIncounterForm;
        rb2d.gravityScale = 0f;
        yield return waitWakeUpFromSleep;
        animator.enabled = true;
        animator.SetBool("isAlreadyAwake", true);
        isAlreadyAwake = true; //행동 개시
        yield return waitForEndOfFrame;
        wakeUpCoroutine = null;
    }

    private void FixedUpdate()
    {
        if (!isAlreadyAwake)
        {
            return;
        }
        if (dashCoroutine == null)
        {
            MoveForward();
        }
    }

    private void Update()
    {
        SearchPlayer();
        UpdateSpriteState();
        if (!isAlreadyAwake)
        {
            if (detectPlayer)
            {
                if (wakeUpCoroutine == null)
                {
                    wakeUpCoroutine = StartCoroutine(WakeUpFromSleep());
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        if (isAlreadyAwake)
        {
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
