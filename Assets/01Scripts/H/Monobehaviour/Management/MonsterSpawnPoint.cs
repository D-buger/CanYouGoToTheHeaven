using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    MonsterManager monsterManager;

    public string grade;
    [SerializeField, Tooltip("������")] string monsterToSpawn;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) //�÷��̾ ���� ���� ������
        {
            if (monsterToSpawn == null)
            {
                Debug.LogError("���Ͱ� �Ҵ���� �ʾҽ��ϴ�!");
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
        if (StageManager.Instance.PlayerRoom <= 3) //1STAGE
        {
            grade = "D";
        }
        else if (StageManager.Instance.PlayerRoom <= 9)
        {
            grade = "C";
        }
        else if (StageManager.Instance.PlayerRoom <= 12)
        {
            grade = "B";
        }
        else if (StageManager.Instance.PlayerRoom > 12)
        {
            grade = "A";
        }
        List<string> monsterList = monsterManager.GetMonstersNameWithGrade(grade);
        int index = Random.Range(0, monsterList.Count);

        monsterToSpawn = monsterList[index];
    }

    void SpawnMonster()
    {
        GetMonsterToSpawn();
        GameObject spawnedMonster = MonsterPoolManager.instance.GetObject(monsterToSpawn);
        MonsterManager.instance.ReportMonsterSpawned(spawnedMonster);
        HMonster spawnedMonsterComp = spawnedMonster.GetComponent<HMonster>();

        spawnedMonster.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float chan = monsterManager.player.GetComponent<Player>().stats.chanceOfSpawnGoldenMonster;
        chan = 80f;
        if (Probability(chan))
        {
            spawnedMonsterComp.MakeGoldenMonster();
        }
        spawnedMonsterComp.OperateOnEnable();
        Destroy(gameObject);
    }

    bool Probability(float _percentage)
    {
        int maxRange = 10000;
        int pickedValue = Random.Range(0, maxRange);
        int chance = (int)(((System.Math.Truncate(_percentage * 100f) * 0.01f) * 100f) + 1);

        return pickedValue < chance ? true : false;
    }
}
