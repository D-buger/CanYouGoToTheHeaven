using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    public enum Grade
    {
        D, C, B, A, S, SS, SSS
    }

    MonsterManager monsterManager;
    [SerializeField] Grade grade;

    [SerializeField, Tooltip("디버깅용")] string monsterToSpawn;

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //플레이어가 범위 내에 들어오면
        {
            if (monsterToSpawn == null)
            {
                Debug.LogError("몬스터가 할당되지 않았습니다!");
                return;
            }
            SpawnMonster();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterManager = MonsterManager.instance;
        GetMonsterToSpawn();
    }

    void GetMonsterToSpawn()
    {
        monsterManager.GetMonstersNameWithGrade(grade.ToString);
    }

    void SpawnMonster()
    {
        GameObject spawnedMonster = MonsterPoolManager.instance.GetObject(monsterToSpawn);
        HMonster spawnedMonsterComp = spawnedMonster.GetComponent<HMonster>();

        spawnedMonster.transform.position = transform.position;

        Debug.LogWarning($"{gameObject.name}수정할 내용 있음");
        if (Probability(75f/*플레이어의 현재 황금몹 스폰 확률을 넣을것!!!*/))
        {
            Debug.Log("황금몬스터가 되었습니다!"); //디버그 용이니 지울것
            spawnedMonsterComp.MakeGoldenMonster();
        }

        spawnedMonsterComp.OperateOnEnable();
        Destroy(gameObject);
    }

    bool Probability(float _percentage)
    {
        bool result = false;
        int maxRange = 10000;
        int pickedValue = Random.Range(0, maxRange);
        int chance = (int)(((System.Math.Truncate(_percentage * 100f) * 0.01f) * 100f) + 1);

        return pickedValue < chance ? true : false;
    }
}
