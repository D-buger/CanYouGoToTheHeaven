using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    MonsterManager monsterManager;

    [SerializeField, Range(0.0f, 100.0f), Tooltip("황금 몬스터가 스폰될 백분율")] float aChanceOfSpawningGoldenMonster = 2.5f;
    [SerializeField, Tooltip("디버깅용")] string monsterToSpawn;

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            if (monsterToSpawn == null)
            {
                return;
            }
            SpawnMonster();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterManager = MonsterManager.instance;
        Debug.Log(monsterToSpawn);
    }

    WaitForSeconds wait2Sec = new WaitForSeconds(2f);

    void SpawnMonster()
    {
        GameObject spawnedMonster = MonsterPoolManager.instance.GetObject($"{monsterToSpawn}");
        spawnedMonster.transform.position = transform.position;
        bool isGoldenMonster = Probability(99f/*플레이어의 현재 황금몹 스폰 확률을 넣을것!!!*/);
        Debug.LogWarning("수정할 내용 있음");

        if (isGoldenMonster)
        {
            spawnedMonster.GetComponent<HMonster>().MakeGoldenMonster();
        }
        Destroy(gameObject);
    }

    bool Probability(float _percentage)
    {
        bool result = false;
        int maxRange = 10000;
        int pickedValue = Random.Range(0, maxRange);
        int chance = (int)(((System.Math.Truncate(_percentage * 100f) * 0.01f) * 100f) + 1);
        if (pickedValue < chance)
        {
            result = true;
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SpawnMonster();
        }
    }
}
