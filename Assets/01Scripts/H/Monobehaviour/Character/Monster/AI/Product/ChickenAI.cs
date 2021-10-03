using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAI : PatrolMonster
{
    [SerializeField] GameObject chickPrefab;
    [SerializeField] float chickSpawnDelay;
    WaitForSeconds SpawnDelayCount;
    Coroutine spawnChick;
    bool alreadyDetectPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        OperateStart();
        SpawnDelayCount = new WaitForSeconds(chickSpawnDelay);
        StartCoroutine(RemoveGravity());
    }

    private void Awake()
    {
        OperateAwake();
    }

    private void OnEnable()
    {
        OperateOnEnable();
    }

    IEnumerator RemoveGravity()
    {
        yield return waitFor1Seconds;
        rb2d.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFront();
        CheckDistanceFromPlayer();
        if (spawnChick != null) //이미 소환 작업에 들어갔다면
        {
            return;
        }
        if (!alreadyDetectPlayer)
        {
            SearchPlayer();
            if (detectPlayer)
            {
                alreadyDetectPlayer = true;
                transform.position = (new Vector2(transform.position.x, transform.position.y - 0.3f));
                movementSpeed = movementSpeed * 0.5f;
                spawnChick = StartCoroutine(SpawnChick());
            }
        }
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    IEnumerator SpawnChick()
    {
        animator.SetBool("detectPlayer", true);
        while (true)
        {
            yield return SpawnDelayCount;
            GameObject chick = MonsterPoolManager.instance.GetObject("병아리");
            chick.transform.position = transform.position;
        }
    }

    private void OnDisable()
    {
        if (spawnChick != null)
        {
            StopCoroutine(spawnChick);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
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
