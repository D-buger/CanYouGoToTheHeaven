using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnMonsterGradeType
{
    E, D, C, B, A, S
}

public class MonsterSpawnPoint : MonoBehaviour
{
    [SerializeField] SpawnMonsterGradeType spawnMonsterGradeType;
    MonsterGradeManager monsterGradeManager;
    GameObject[] monsterList;
    [SerializeField] GameObject monsterToCreat;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            if (monsterToCreat == null)
            {
                Debug.LogError($"({gameObject.transform.position}) {gameObject.name}: ������ ���Ͱ� �����ϴ�!");
                return;
            }
            SpawnMonster();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterGradeManager = MonsterGradeManager.instance;
        StartCoroutine(FindArray());
    }

    WaitForSeconds wait2Sec = new WaitForSeconds(2f);

    IEnumerator FindArray() //!!!!dictionary Ȱ���ؼ� �̸� �ٸ� �ڷ��� ������ ���� �� ����?!!!!
    {
        yield return wait2Sec;
        switch ((int)spawnMonsterGradeType)
        {
            case 0: monsterList = monsterGradeManager.EGradeMonster;
                break; 
            case 1: monsterList = monsterGradeManager.DGradeMonster;
                break;
            case 2: monsterList = monsterGradeManager.CGradeMonster;
                break;
            case 3: monsterList = monsterGradeManager.BGradeMonster;
                break;
            case 4: monsterList = monsterGradeManager.AGradeMonster;
                break;
            case 5: monsterList = monsterGradeManager.SGradeMonster;
                break;
            default: Debug.LogError($"{gameObject.name}: Not Implemented!");
                break;
        }
        int index = Random.Range(0, monsterList.Length);
        Debug.Log($"index is {index}");
        if (monsterList[index] == null)
        {
            Debug.LogError("���͸� ã�� ����");
            yield break;
        }
        monsterToCreat = monsterList[index];
    }

    void SpawnMonster()
    {
        if (monsterToCreat == null)
        {
            Debug.LogError("��ȯ��ų ���Ͱ� ����");
            return;
        }
        GameObject spawnedMonster = Instantiate(monsterToCreat);
        spawnedMonster.transform.position = transform.position;
        Destroy(gameObject);
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
