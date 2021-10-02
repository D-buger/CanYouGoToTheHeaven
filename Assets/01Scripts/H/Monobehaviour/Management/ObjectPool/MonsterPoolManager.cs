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
    [SerializeField, Tooltip("'시작할 때' 해당 오브젝트가 풀링됨")] List<ObjectList> objectList = new List<ObjectList>();

    int currentIndex = 0;
    Dictionary<string, int> poolIndexDictionary = new Dictionary<string, int>(); //string을 받으면 해당 이름을 가진 오브젝트가 몇 번째 풀에 있는지
    List<MonsterPool> monsterPoolComponents; //각 풀들의 컴포넌트 캐싱용. 위의 딕셔너리에서 받은 index에 해당하는 값을 가져오면 됨

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
            Debug.LogWarning($"{gameObject.name}: 오브젝트 풀 프리팹이 할당되지 않았습니다");
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
        Debug.Log($"{objectName}의 풀을 만들었습니다");
        if (poolIndexDictionary.ContainsKey(objectName)) //해당 키가 있으면 = 해당 오브젝트가 이미 풀링되어 있으면
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
