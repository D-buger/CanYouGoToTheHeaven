using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBirdAI : HMonster
{
    WaitForSeconds waitForExplosionDelay;

    [SerializeField, Range(0f, 2f), Tooltip("점화 애니메이션이 모두 완료된 후 폭발까지의 딜레이")] float explosionDelay;
    [SerializeField] float detectPlayerRange;
    [SerializeField] int fragmentDamage;
    [SerializeField] float fragmentVelocity;

    CircleCollider2D circle;
    float radius;
    bool detectPlayer;

    protected override void SettingVariables()
    {
        base.SettingVariables();
        detectPlayerRange = StringToFloat(GetDataWithVariableName("CognitiveRange"));
        fragmentVelocity = StringToFloat(GetDataWithVariableName("ProjectileVelocity"));
        fragmentDamage = StageManager.Instance.PlayerRoom <= 3 ? 1 : 2;
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    private void Awake()
    {
        OperateAwake();
    }

    void Start()
    {
        OperateStart();
        waitForExplosionDelay = new WaitForSeconds(explosionDelay);
        circle = GetComponent<CircleCollider2D>();
        radius = circle.radius + 0.15f;
    }

    private void Update()
    {
        OperateUpdate();
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            detectPlayer = true;
            StartCoroutine(Explosive());
        }
        if (!detectPlayer)
        {
            CheckPlayer();
        }
    }

    RaycastHit2D rightRay;
    RaycastHit2D leftRay;

    void CheckPlayer() //circle의 radius만큼 지나서 시작: 본인에게 닿는 문제 해결
    {
        rightRay = Physics2D.Raycast(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right, detectPlayerRange);
        leftRay = Physics2D.Raycast(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left, detectPlayerRange);
        Debug.DrawRay(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right * detectPlayerRange, Color.magenta);
        Debug.DrawRay(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left * detectPlayerRange, Color.magenta);
        if (rightRay.collider != null && rightRay.collider.CompareTag("Player"))
        {
            detectPlayer = true;
            StartCoroutine(Explosive());
        }
        if (leftRay.collider != null && leftRay.collider.CompareTag("Player"))
        {
            detectPlayer = true;
            StartCoroutine(Explosive());
        }
    }

    WaitForSeconds waitFor500MilliSecond = new WaitForSeconds(0.5f);

    IEnumerator Explosive()
    {
        animator.SetBool("detectPlayer", true);
        yield return waitForEndOfFrame;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            yield return waitForEndOfFrame;
        }
        yield return waitForExplosionDelay;
        animator.SetBool("ignitionIsDone", true);

        for (int i = 0; i < 2; i++)
        {
            GameObject fragment = MonsterPoolManager.instance.GetObject("BombBirdExplosionFragment");
            fragment.transform.position = transform.position;
            if (i == 1)
            {
                fragment.GetComponent<Fragment>().Initialize(fragmentDamage, fragmentVelocity);
            }
            else
            {
                fragment.GetComponent<Fragment>().Initialize(fragmentDamage, -fragmentVelocity);
                fragment.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        yield return waitFor500MilliSecond;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            yield return waitForEndOfFrame;
        }
        DespawnMonster();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }
}
