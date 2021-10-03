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
    List<MonsterPool> monsterPoolComponents = new List<MonsterPool>(); //�� Ǯ���� ������Ʈ ĳ�̿�. ���� ��ųʸ����� ���� index�� �ش��ϴ� ���� �������� ��

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
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

        for (int i = 0; i < objectList.Count; i++)
        {
            CreateObjectPools(objectList[i].objectToPooling, objectList[i].poolingCount);
        }
    }

    void CreateObjectPools(GameObject _objectToPooling, int _poolingCount, string _keyName = null)
    {
        string poolingObjectName;
        if (_keyName == null)
        {
            poolingObjectName = _objectToPooling.name;
        }
        else
        {
            poolingObjectName = _keyName;
        }

        if (poolIndexDictionary.ContainsKey(poolingObjectName))
        {
            Debug.LogWarning($"�̹� '{poolingObjectName}'Ű�� �����մϴ�!");
            return;
        }

        GameObject pool = Instantiate(objectPoolPrefab, transform);
        
        pool.name = poolingObjectName + "Pool";

        poolIndexDictionary.Add(poolingObjectName, currentIndex);
        monsterPoolComponents.Insert(currentIndex, pool.GetComponent<MonsterPool>());
        monsterPoolComponents[currentIndex].StartingInitialize(_objectToPooling, _poolingCount);

        currentIndex += 1;
    }

    public void RequestAddObjectPool(GameObject _objectToPooling, int _poolingCount, string _customKeyName = null)
    {
        string objectName;
        if (_customKeyName == null)
        {
            objectName = _objectToPooling.name;
        }
        else
        {
            objectName = _customKeyName;
        }

        if (poolIndexDictionary.ContainsKey(objectName)) //�ش� Ű�� ������ = �ش� ������Ʈ�� �̹� Ǯ���Ǿ� ������
        {
            Debug.Log($"�̹� ������ Ű�� �����մϴ�!");
            return;
        }
        CreateObjectPools(_objectToPooling, _poolingCount, _customKeyName);
    }

    public GameObject GetObject(string _objectName)
    {
        if (!poolIndexDictionary.ContainsKey(_objectName))
        {
            Debug.LogWarning($"'{_objectName}'������Ʈ�� Ǯ���� ã�� ���Ͽ����ϴ�.");
            return null;
        }

        int index = poolIndexDictionary[_objectName];
        GameObject obj = monsterPoolComponents[index].GetObject();
        return obj;
    }

    public void ReturnObject(GameObject _object, string _customObjectName = null)
    {
        string objectName;
        if (_customObjectName == null)
        {
            objectName = _object.name;
        }
        else
        {
            objectName = _customObjectName;
        }
        if (!poolIndexDictionary.ContainsKey(objectName))
        {
            RequestAddObjectPool(_object, 3);
        }

        int index = poolIndexDictionary[objectName];
        monsterPoolComponents[index].ReturnObject(_object);
    }
}
