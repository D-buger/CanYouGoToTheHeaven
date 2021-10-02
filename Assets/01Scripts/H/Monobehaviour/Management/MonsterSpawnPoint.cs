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

    [SerializeField, Tooltip("������")] string monsterToSpawn;

    private void OnTriggerStay2D(Collider2D _collision)
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
        monsterManager.GetMonstersNameWithGrade(grade.ToString);
    }

    void SpawnMonster()
    {
        GameObject spawnedMonster = MonsterPoolManager.instance.GetObject(monsterToSpawn);
        HMonster spawnedMonsterComp = spawnedMonster.GetComponent<HMonster>();

        spawnedMonster.transform.position = transform.position;

        Debug.LogWarning($"{gameObject.name}������ ���� ����");
        if (Probability(75f/*�÷��̾��� ���� Ȳ�ݸ� ���� Ȯ���� ������!!!*/))
        {
            Debug.Log("Ȳ�ݸ��Ͱ� �Ǿ����ϴ�!"); //����� ���̴� �����
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
