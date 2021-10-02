using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoBehaviour
{
    public static MonsterPoolManager instance = null;

    [System.Serializable]
    class ObjectList
    {
        public GameObject objectToPooling;
        public int poolingCount = 5;
    }

    [SerializeField] GameObject objectPoolPrefab;
    [Space]
    [SerializeField, Tooltip("'������ ��' �ش� ������Ʈ�� Ǯ����")] List<ObjectList> objectList = new List<ObjectList>();

    int currentIndex = 0;
    Dictionary<string, int> poolIndexDictionary = new Dictionary<string, int>(); //string�� ������ �ش� �̸��� ���� ������Ʈ�� �� ��° Ǯ�� �ִ���
    List<MonsterPool> monsterPoolComponents; //�� Ǯ���� ������Ʈ ĳ�̿�. ���� ��ųʸ����� ���� index�� �ش��ϴ� ���� �������� ��

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

    private void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        if (objectPoolPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: ������Ʈ Ǯ �������� �Ҵ���� �ʾҽ��ϴ�");
            return;
        }
        monsterPoolComponents = new List<MonsterPool>();

        for (int i = 0; i < objectList.Count; i++)
        {
            CreateObjectPools(objectList[i].objectToPooling, objectList[i].poolingCount);
        }
    }

    void CreateObjectPools(GameObject _objectToPooling, int _poolingCount)
    {
        GameObject pool = Instantiate(objectPoolPrefab, transform);
        string poolingObjectName = _objectToPooling.name;
        pool.name = poolingObjectName + "Pool";

        poolIndexDictionary.Add(poolingObjectName, currentIndex);
        monsterPoolComponents.Insert(currentIndex, pool.GetComponent<MonsterPool>());
        monsterPoolComponents[currentIndex].StartingInitialize(_objectToPooling, _poolingCount);

        currentIndex += 1;
    }

    public void RequestAddObjectPool(GameObject _objectToPooling, int _poolingCount)
    {
        string objectName = _objectToPooling.name;
        Debug.Log($"{objectName}�� Ǯ�� ��������ϴ�");
        if (poolIndexDictionary.ContainsKey(objectName)) //�ش� Ű�� ������ = �ش� ������Ʈ�� �̹� Ǯ���Ǿ� ������
        {
            return;
        }
        CreateObjectPools(_objectToPooling, _poolingCount);
    }

    public GameObject GetObject(string _objectName)
    {
        int index = poolIndexDictionary[_objectName];
        GameObject obj = monsterPoolComponents[index].GetObject();
        return obj;
    }

    public void ReturnObject(GameObject _object)
    {
        string objectName = _object.name;
        int index = poolIndexDictionary[objectName];
        monsterPoolComponents[index].ReturnObject(_object);
    }
}
