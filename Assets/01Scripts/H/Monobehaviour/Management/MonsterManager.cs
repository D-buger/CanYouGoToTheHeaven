using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV = new List<Dictionary<string, string>>(); //CSV파일 읽어온 것
    int dataSize = 0;


    Dictionary<string, int> monsterIndexDictionary = new Dictionary<string, int>(); //key값으로 몬스터의 이름을 넣으면 value로 integer인 index를 반환
    Dictionary<string, List<string>> monstersDivideAsGrade = new Dictionary<string, List<string>>(); //key값으로 등급을 입력받으면 value로 해당 등급의 몬스터들의 배열을 반환

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
    public GameObject goldenMonsterParticle;

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어를 찾지 못함!");
        }
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
        foreach (var item in monsterIndexDictionary)
        {
            Debug.Log(item);
        }
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
            Debug.Log($"{monsterList[0]}입니당");

            MonsterPoolManager.instance.RequestAddObjectPool(monsterList[i], 5);
        }
    }

    void ReadingCSVFile()
    {
        monsterInfoCSV = CSVReader.Read("MonsterData", out dataSize);
        dataSize -= 2;
        Debug.Log($"{dataSize}사이즈");
        int currentReadRowIndex = 0;
        for (int i = 0; i < dataSize; i++)
        {
            string monsterName = monsterInfoCSV[i]["MonsterName"];
            Debug.Log($"{i}번쨰는 {monsterName}의 차지입니다");
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i번째 행의 몬스터 이름을 key로, i를 value로 딕셔너리에 추가
            currentReadRowIndex += 1;

            /*if (i < monsterInfoCSV[i].Count + 2)
            {
                string monsterGrade = monsterInfoCSV[i]["MonsterGrade"]; //등급을 넣으면 해당 등급의 몬스터들의 이름이 나옴
                Debug.Log($"{monsterInfoCSV[i]["MonsterGrade"]}등급: {monsterInfoCSV[i]["MonsterName"]}");
                if (monstersDivideAsGrade.ContainsKey(monsterGrade)) //이미 해당 등급의 키를 가지고 있다면
                {
                    monstersDivideAsGrade[monsterGrade].Insert(monstersDivideAsGrade[monsterGrade].Count, monsterInfoCSV[i]["MonsterName"]);
                    Debug.Log("기존의 키에 추가했습니다");
                }
                else //해당 등급이 처음 추가되는거라면
                {
                    monstersDivideAsGrade.Add(monsterGrade, new List<string>());
                    monstersDivideAsGrade[monsterGrade].Insert(0, monsterInfoCSV[i]["MonsterName"]);
                    Debug.Log("새 리스트를 만들었습니다");
                }
                currentReadRowIndex += 1;
            }*/
        }
        Debug.Log(currentReadRowIndex);
    }

    public Dictionary<string, string> GetDataWithMonsterName(string _name) //몬스터의 string형 이름을 받으면(오브젝트 이름 X)
    {
        int index = monsterIndexDictionary[_name]; //해당 몬스터의 데이터가 몇 번째 행인지 구함
        return monsterInfoCSV[index];
    }

    public List<string> GetMonstersNameWithGrade(string _grade) //몬스터 등급을 입력 받으면 해당 등급의 몬스터 이름 배열을 반환
    {
        return monstersDivideAsGrade[_grade];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '보물방 포탈 프리팹'이 할당되어있지 않습니다!");
            return;
        }
        //MonsterPoolManager.instance.RequestAddObjectPool(TreasureRoomPortalPrefab, 2);

        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: '황금 몬스터 파티클'이 할당되어있지 않습니다!");
            return;
        }
        //MonsterPoolManager.instance.RequestAddObjectPool(goldenMonsterParticle, 10);
    }
}
