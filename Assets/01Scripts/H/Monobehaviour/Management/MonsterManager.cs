using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV; //CSV파일 읽어온 것
    int dataSize = 0;


    Dictionary<string, int> monsterIndexDictionary; //key값으로 몬스터의 이름을 넣으면 value로 integer인 index를 반환
    Dictionary<string, List<string>> monstersNameDivideAsGradeDic; //key값으로 등급을 입력받으면 value로 해당 등급의 몬스터들의 배열을 반환

    List<GameObject> spawnedMonsterList = new List<GameObject>(); //현재 스테이지에서 소환된 애들을 모아두는 곳. 스테이지가 넘어갈 때 마다 해당 리스트 안의 몬스터를 전부 풀로 돌려보낸다

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject[] managers;
    [HideInInspector] public GameObject player { get; private set; }
    [Space, SerializeField] List<GameObject> monsterList = new List<GameObject>();
    [Space]
    public GameObject TreasureRoomPortalPrefab;
    public GameObject tempPlatform;
    public GameObject goldenMonsterParticle;

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어를 찾지 못함!");
        }
    }

    public void ReportMonsterSpawned(GameObject _monster)
    {
        spawnedMonsterList.Add(_monster);
    }

    public void StageChanged()
    {
        int count = 0;
        for (int i = 0; i < spawnedMonsterList.Count; i++)
        {
            GameObject monster = spawnedMonsterList[i];
            if (monster.activeSelf == true)
            {
                monster.GetComponent<HMonster>().DespawnMonster();
                count += 1;
            }
        }
        spawnedMonsterList.RemoveAll(x => true);
        Debug.Log($"총 {count}만큼 돌려보냄");
    }

    void AddEventHandler()
    {
        StageManager.changeCurrentRoom += (int _value) => StageChanged();
    }

    private void Awake()
    {
        MakeSingleton();
        ReadingCSVFile();
        CreatingManagers();
    }

    private void Start()
    {
        FindPlayer();
        RequestPoolingMonsters();
        CheckVariable();
        AddEventHandler();
    }

    void CreatingManagers()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            if (managers[i] == null)
            {
                break;
            }
            Instantiate(managers[i]);
        }
    }

    private void RequestPoolingMonsters()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i] == null)
            {
                break;
            }

            MonsterPoolManager.instance.RequestAddObjectPool(monsterList[i], 5, monsterList[i].GetComponent<HMonster>().monsterName);
        }
    }

    void ReadingCSVFile()
    {
        monsterInfoCSV = new List<Dictionary<string, string>>();
        monsterIndexDictionary = new Dictionary<string, int>();
        monstersNameDivideAsGradeDic = new Dictionary<string, List<string>>();

        monsterInfoCSV = CSVReader.Read("MonsterData", out dataSize);
        dataSize -= 2;
        int currentReadRowIndex = 0;
        for (int i = 0; i < dataSize; i++)
        {
            string monsterName = monsterInfoCSV[i]["MonsterName"];
            string monsterGrade = monsterInfoCSV[i]["MonsterGrade"];
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i번째 행의 몬스터 이름을 key로, i를 value로 딕셔너리에 추가

            if (monstersNameDivideAsGradeDic.ContainsKey(monsterGrade)) //해당 등급이 이미 있으면
            {
                List<string> monsterNameDivideAsGradeList = monstersNameDivideAsGradeDic[monsterGrade];
                monsterNameDivideAsGradeList.Insert(monsterNameDivideAsGradeList.Count, monsterName);
                monstersNameDivideAsGradeDic.Remove(monsterGrade);
                monstersNameDivideAsGradeDic.Add(monsterGrade, monsterNameDivideAsGradeList);
            }
            else //처음 추가되는 등급이라면
            {
                List<string> monsterNameDivideAsGradeList = new List<string>();
                monsterNameDivideAsGradeList.Insert(0, monsterName);
                monstersNameDivideAsGradeDic.Add(monsterGrade, monsterNameDivideAsGradeList);
            }

            currentReadRowIndex += 1;
        }
    }

    public Dictionary<string, string> GetDataWithMonsterName(string _name) //몬스터의 string형 이름을 받으면(오브젝트 이름 X)
    {
        if (!monsterIndexDictionary.ContainsKey(_name))
        {
            Debug.LogWarning("해당 몬스터는 등록되지 않은 몬스터입니다. CSV파일을 확인하세요");
            return null;
        }
        int index = monsterIndexDictionary[_name]; //해당 몬스터의 데이터가 몇 번째 행인지 구함
        return monsterInfoCSV[index];
    }

    public List<string> GetMonstersNameWithGrade(string _grade) //몬스터 등급을 입력 받으면 해당 등급의 몬스터 이름 배열을 반환
    {
        return monstersNameDivideAsGradeDic[_grade];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '보물방 포탈 프리팹'이 할당되어있지 않습니다!");
            return;
        }
        MonsterPoolManager.instance.RequestAddObjectPool(TreasureRoomPortalPrefab, 2);

        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: '황금 몬스터 파티클'이 할당되어있지 않습니다!");
            return;
        }
        MonsterPoolManager.instance.RequestAddObjectPool(goldenMonsterParticle, 6);
    }
}
