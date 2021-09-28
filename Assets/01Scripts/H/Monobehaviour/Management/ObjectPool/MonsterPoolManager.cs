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
    [SerializeField] List<ObjectList> objectList = new List<ObjectList>();

    Dictionary<string, int> poolIndexDictionary = new Dictionary<string, int>(); //string�� ������ �ش� �̸��� ���� ������Ʈ�� �� ��° Ǯ�� �ִ���
    MonsterPool[] monsterPoolComponents; //�� Ǯ���� ������Ʈ ĳ�̿�

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

    void AddPoolingObject() //CSV���� �о�� ������ ���� ���͵��� �Ҵ�����ִ� ����
    {

    }

    void CreateObjectPools()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            GameObject pool = Instantiate(objectPoolPrefab, transform);

            string poolingObjectName = objectList[i].objectToPooling.name;
            pool.name = poolingObjectName + "Pool";

            poolIndexDictionary.Add(poolingObjectName, i);
            monsterPoolComponents[i] = pool.GetComponent<MonsterPool>();
            monsterPoolComponents[i].StartingInitialize(objectList[i].objectToPooling, objectList[i].poolingCount);
        }
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

    // Start is called before the first frame update
    void Start()
    {
        if (objectPoolPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: ������Ʈ Ǯ �������� �Ҵ���� �ʾҽ��ϴ�");
        }
        monsterPoolComponents = new MonsterPool[objectList.Count];
        CreateObjectPools();
    }
}
