using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV; //CSV���� �о�� ��
    int dataSize = 0;


    Dictionary<string, int> monsterIndexDictionary; //key������ ������ �̸��� ������ value�� integer�� index�� ��ȯ
    Dictionary<string, List<string>> monstersNameDivideAsGradeDic; //key������ ����� �Է¹����� value�� �ش� ����� ���͵��� �迭�� ��ȯ

    List<GameObject> spawnedMonsterList = new List<GameObject>(); //���� ������������ ��ȯ�� �ֵ��� ��Ƶδ� ��. ���������� �Ѿ �� ���� �ش� ����Ʈ ���� ���͸� ���� Ǯ�� ����������

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
            Debug.LogWarning($"{gameObject.name}: �÷��̾ ã�� ����!");
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
        Debug.Log($"�� {count}��ŭ ��������");
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
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i��° ���� ���� �̸��� key��, i�� value�� ��ųʸ��� �߰�

            if (monstersNameDivideAsGradeDic.ContainsKey(monsterGrade)) //�ش� ����� �̹� ������
            {
                List<string> monsterNameDivideAsGradeList = monstersNameDivideAsGradeDic[monsterGrade];
                monsterNameDivideAsGradeList.Insert(monsterNameDivideAsGradeList.Count, monsterName);
                monstersNameDivideAsGradeDic.Remove(monsterGrade);
                monstersNameDivideAsGradeDic.Add(monsterGrade, monsterNameDivideAsGradeList);
            }
            else //ó�� �߰��Ǵ� ����̶��
            {
                List<string> monsterNameDivideAsGradeList = new List<string>();
                monsterNameDivideAsGradeList.Insert(0, monsterName);
                monstersNameDivideAsGradeDic.Add(monsterGrade, monsterNameDivideAsGradeList);
            }

            currentReadRowIndex += 1;
        }
    }

    public Dictionary<string, string> GetDataWithMonsterName(string _name) //������ string�� �̸��� ������(������Ʈ �̸� X)
    {
        if (!monsterIndexDictionary.ContainsKey(_name))
        {
            Debug.LogWarning("�ش� ���ʹ� ��ϵ��� ���� �����Դϴ�. CSV������ Ȯ���ϼ���");
            return null;
        }
        int index = monsterIndexDictionary[_name]; //�ش� ������ �����Ͱ� �� ��° ������ ����
        return monsterInfoCSV[index];
    }

    public List<string> GetMonstersNameWithGrade(string _grade) //���� ����� �Է� ������ �ش� ����� ���� �̸� �迭�� ��ȯ
    {
        return monstersNameDivideAsGradeDic[_grade];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '������ ��Ż ������'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
            return;
        }
        MonsterPoolManager.instance.RequestAddObjectPool(TreasureRoomPortalPrefab, 2);

        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: 'Ȳ�� ���� ��ƼŬ'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
            return;
        }
        MonsterPoolManager.instance.RequestAddObjectPool(goldenMonsterParticle, 6);
    }
}
