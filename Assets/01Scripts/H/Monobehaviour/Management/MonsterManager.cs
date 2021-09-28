using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public List<Dictionary<string, string>> monsterInfoCSV = new List<Dictionary<string, string>>(); //CSV���� �о�� ��
    Dictionary<string, int> monsterIndexDictionary = new Dictionary<string, int>(); //key������ ������ �̸��� ������ value�� integer�� index�� ��ȯ

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
    //Dictionary Ȱ���ؼ� ������ �����?

    [HideInInspector] public GameObject player{ get; private set; }
    public GameObject TreasureRoomPortalPrefab;
    public GameObject goldenMonsterParticle;

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        Debug.LogWarning("�÷��̾ ������ Ư�� �Ŵ������� ���� �޾ƿð�!");

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
            if (monsterInfoCSV[i]["Index"] == "" || monsterInfoCSV[i] == null) //�ε����� �����̸� == �ش� �࿡ �����Ͱ� ���ٸ�
            {
                break; //Escape
            }
            monsterIndexDictionary.Add(monsterInfoCSV[i]["MonsterName"], i); //i��° ���� ���� �̸��� key��, i�� value�� ��ųʸ��� �߰�
        }
    }

    public Dictionary<string, string> GetDataWithName(string _name)
    {
        int index = monsterIndexDictionary[_name]; //�� ��° ������ ����
        return monsterInfoCSV[index];
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '������ ��Ż ������'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
        }
        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: 'Ȳ�� ���� ��ƼŬ'�� �Ҵ�Ǿ����� �ʽ��ϴ�!");
        }
    }

    private void Awake()
    {
        MakeSingleton();
    }
}
