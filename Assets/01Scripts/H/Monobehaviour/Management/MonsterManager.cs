using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV = new List<Dictionary<string, string>>(); //CSV���� �о�� ��
    int dataSize = 0;


    Dictionary<string, int> monsterIndexDictionary = new Dictionary<string, int>(); //key������ ������ �̸��� ������ value�� integer�� index�� ��ȯ
    Dictionary<string, List<string>> monstersDivideAsGrade = new Dictionary<string, List<string>>(); //key������ ����� �Է¹����� value�� �ش� ����� ���͵��� �迭�� ��ȯ

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
            Debug.LogWarning($"{gameObject.name}: �÷��̾ ã�� ����!");
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
            Debug.Log($"{monsterList[0]}�Դϴ�");

            MonsterPoolManager.instance.RequestAddObjectPool(monsterList[i], 5);
        }
    }

    void ReadingCSVFile()
    {
        monsterInfoCSV = CSVReader.Read("MonsterData", out dataSize);
        dataSize -= 2;
        Debug.Log($"{dataSize}������");
        int currentReadRowIndex = 0;
        for (int i = 0; i < dataSize; i++)
        {
            string monsterName = monsterInfoCSV[i]["MonsterName"];
            Debug.Log($"{i}������ {monsterName}�� �����Դϴ�");
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i��° ���� ���� �̸��� key��, i�� value�� ��ųʸ��� �߰�
            currentReadRowIndex += 1;

            /*if (i < monsterInfoCSV[i].Count + 2)
            {
                string monsterGrade = monsterInfoCSV[i]["MonsterGrade"]; //����� ������ �ش� ����� ���͵��� �̸��� ����
                Debug.Log($"{monsterInfoCSV[i]["MonsterGrade"]}���: {monsterInfoCSV[i]["MonsterName"]}");
                if (monstersDivideAsGrade.ContainsKey(monsterGrade)) //�̹� �ش� ����� Ű�� ������ �ִٸ�
                {
                    monstersDivideAsGrade[monsterGrade].Insert(monstersDivideAsGrade[monsterGrade].Count, monsterInfoCSV[i]["MonsterName"]);
                    Debug.Log("������ Ű�� �߰��߽��ϴ�");
                }
                else //�ش� ����� ó�� �߰��Ǵ°Ŷ��
                {
                    monstersDivideAsGrade.Add(monsterGrade, new List<string>());
                    monstersDivideAsGrade[monsterGrade].Insert(0, monsterInfoCSV[i]["MonsterName"]);
                    Debug.Log("�� ����Ʈ�� ��������ϴ�");
                }
                currentReadRowIndex += 1;
            }*/
        }
        Debug.Log(currentReadRowIndex);
    }

    public Dictionary<string, string> GetDataWithMonsterName(string _name) //������ string�� �̸��� ������(������Ʈ �̸� X)
    {
        int index = monsterIndexDictionary[_name]; //�ش� ������ �����Ͱ� �� ��° ������ ����
        return monsterInfoCSV[index];
    }

    public List<string> GetMonstersNameWithGrade(string _grade) //���� ����� �Է� ������ �ش� ����� ���� �̸� �迭�� ��ȯ
    {
        return monstersDivideAsGrade[_grade];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '������ ��Ż ������'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
            return;
        }
        //MonsterPoolManager.instance.RequestAddObjectPool(TreasureRoomPortalPrefab, 2);

        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: 'Ȳ�� ���� ��ƼŬ'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
            return;
        }
        //MonsterPoolManager.instance.RequestAddObjectPool(goldenMonsterParticle, 10);
    }
}
