using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAI : WalkMonster
{
    [SerializeField] GameObject chickPrefab;
    [SerializeField] float chickSpawnDelay;
    WaitForSeconds SpawnDelayCount;
    Coroutine spawnChick;

    // Start is called before the first frame update
    void Start()
    {
        OperateStart();
        SpawnDelayCount = new WaitForSeconds(chickSpawnDelay);
        spawnChick = StartCoroutine(SpawnChick());
    }

    // Update is called once per frame
    void Update()
    {
        OperateUpdate();
    }

    private void FixedUpdate()
    {
        OperateFixedUpdate();
    }

    IEnumerator SpawnChick()
    {
        while (true)
        {
            yield return SpawnDelayCount;
            GameObject chick = Instantiate(chickPrefab);
            chick.transform.position = transform.position;
        }
    }

    private void OnDestroy()
    {
        if (spawnChick != null)
        {
            StopCoroutine(spawnChick);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어 충돌 메소드를 플레이어 스크립트로 옮기기 바람");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }
}
