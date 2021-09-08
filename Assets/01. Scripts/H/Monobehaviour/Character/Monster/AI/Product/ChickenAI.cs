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
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            Debug.LogWarning($"{gameObject.name}: �÷��̾� �浹 �޼ҵ带 �÷��̾� ��ũ��Ʈ�� �ű�� �ٶ�");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }
}
