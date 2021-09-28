using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV = new List<Dictionary<string, string>>(); //CSV파일 읽어온 것
    Dictionary<string, int> monsterIndexDictionary = new Dictionary<string, int>(); //key값으로 몬스터의 이름을 넣으면 value로 integer인 index를 반환

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

    public GameObject[] EGradeMonster;
    public GameObject[] DGradeMonster;
    public GameObject[] CGradeMonster;
    public GameObject[] BGradeMonster;
    public GameObject[] AGradeMonster;
    public GameObject[] SGradeMonster;
    public GameObject[] EliteMonster;
    //Dictionary 활용해서 여러개 만들기?

    [HideInInspector] public GameObject player{ get; private set; }
    public GameObject TreasureRoomPortalPrefab;
    public GameObject goldenMonsterParticle;

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        Debug.LogWarning("플레이어를 가급적 특정 매니저에게 직접 받아올것!");

        if (player == null)
        {
            Debug.LogError($"{gameObject.name}: Warnning!! Player is null!");
        }
    }

    private void Start()
    {
        FindPlayer();
        CheckVariable();
    }

    void ReadingCSVFile()
    {
        monsterInfoCSV = CSVReader.Read("MonsterData");
        for (int i = 0; i < 40; i++)
        {
            if (monsterInfoCSV[i]["Index"] == "" || monsterInfoCSV[i] == null) //인덱스가 공란이면 == 해당 행에 데이터가 없다면
            {
                break; //Escape
            }
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i번째 행의 몬스터 이름을 key로, i를 value로 딕셔너리에 추가
        }
    }

    public Dictionary<string, string> GetDataWithName(string _name)
    {
        int index = monsterIndexDictionary[_name]; //몇 번째 행인지 구함
        return monsterInfoCSV[index];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '보물방 포탈 프리팹'이 할당되어있지 않습니다!");
        }
        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: '황금 몬스터 파티클'이 할당되어있지 않습니다!");
        }
    }

    private void Awake()
    {
        MakeSingleton();
    }
}
