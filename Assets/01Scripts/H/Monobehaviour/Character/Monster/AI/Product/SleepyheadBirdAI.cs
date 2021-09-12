using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyheadBirdAI : PatrolMonster
{
    Coroutine dashCoroutine;
    bool isDashing;
    [SerializeField, Range(0.4f, 5f), Tooltip("���ʷ� �÷��̾ �߰��� �� �ῡ�� ���� ������ �ð�")] float wakeUpFromSleep;
    WaitForSeconds waitWakeUpFromSleep;
    Coroutine wakeUpCoroutine = null;
    bool isAlreadyAwake = false;
    [SerializeField, Range(0f, 8f), Tooltip("���� �غ� �ð�(��¡)")] float timeToPrepareDash;
    bool prepareDash;
    [SerializeField, Range(0f, 20f), Tooltip("������ �ӵ�")] float dashingSpeed;
    WaitForSeconds waitPrepareDashDelay;
    [SerializeField, Range(0f, 3f), Tooltip("���� �� ������. �ش� ���µ��� �ƹ��� �ൿ�� �� �� ����.")] float dashAfterCooldown;
    WaitForSeconds waitDashCooldownDelay;
    [SerializeField, Range(0f, 8f), Tooltip("���� ���ӽð�(���� �ӵ��� �Բ� ������ �Ÿ��� �����Ѵ�)")] float dashingDuration;
    WaitForSeconds waitDashingDurationDelay;

    Sprite defaultSprite;
    [SerializeField, Tooltip("�������� * 1.8f�� ������ ���������� ǥ��(���� ��, ���� ����)")] Sprite awakeFromDeepSleep;
    [SerializeField, Tooltip("���������� ���ʷ� ���� ���� ������ ���� ���� ǥ��(���� ����)")] Sprite firstIncounterForm;

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
            Debug.Log("���� �غ�");
            prepareDash = true;
            yield return waitPrepareDashDelay;
            destination = FindDirectionVector(player.transform.position);
            Debug.Log($"��ǥ ����: {destination}");
            Debug.Log("����");
            prepareDash = false;
            isDashing = true;
            rb2d.velocity = destination * dashingSpeed;
            yield return waitDashingDurationDelay;
            Debug.Log("���� ����");
            rb2d.velocity = Vector2.zero;
            isDashing = false;
            yield return waitDashCooldownDelay;
            Debug.Log("���� ���ð� ��");
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
        isAlreadyAwake = true; //�ൿ ����
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
