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
<<<<<<< HEAD
                //Debug.LogError($"({gameObject.transform.position}) {gameObject.name}: 생성할 몬스터가 없습니다!");
=======
>>>>>>> origin/Hyuns
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

<<<<<<< HEAD
    IEnumerator FindArray() //!!!!dictionary 활용해서 이름 다른 자료형 여러개 만들 수 있음?!!!!
    {
        yield return wait2Sec;
        switch ((int)spawnMonsterGradeType)
        {
            case 0: monsterList = monsterManager.EGradeMonster;
                break; 
            case 1: monsterList = monsterManager.DGradeMonster;
                break;
            case 2: monsterList = monsterManager.CGradeMonster;
                break;
            case 3: monsterList = monsterManager.BGradeMonster;
                break;
            case 4: monsterList = monsterManager.AGradeMonster;
                break;
            case 5: monsterList = monsterManager.SGradeMonster;
                break;
            case 6: monsterList = monsterManager.EliteMonster;
                break;
            default: Debug.LogError($"{gameObject.name}: Not Implemented!");
                break;
        }
        int index = Random.Range(0, monsterList.Length);
        //Debug.Log($"index is {index}");
        if (monsterList[index] == null)
        {
            Debug.LogError("몬스터를 찾지 못함");
            yield break;
        }
        monsterToCreat = monsterList[index];
    }

=======
>>>>>>> origin/Hyuns
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
<<<<<<< HEAD
            isGolden = true;
            //Debug.Log($"뽑을 확률은{chance} / {maxRange}이었으며 뽑은 수는{pickedValue} < {chance}이므로 황금 몬스터가 되었습니다");
        }
        else
        {
            //Debug.Log($"뽑을 확률은{chance} / {maxRange}이었으며 뽑은 수는{pickedValue} >= {chance}이므로 황금 몬스터가 되지 못하였습니다");
=======
            result = true;
>>>>>>> origin/Hyuns
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
