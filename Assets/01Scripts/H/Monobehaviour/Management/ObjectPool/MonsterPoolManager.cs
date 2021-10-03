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
    List<MonsterPool> monsterPoolComponents = new List<MonsterPool>(); //각 풀들의 컴포넌트 캐싱용. 위의 딕셔너리에서 받은 index에 해당하는 값을 가져오면 됨

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
            Debug.LogWarning($"{gameObject.name}: 오브젝트 풀 프리팹이 할당되지 않았습니다");
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
            Debug.LogWarning($"이미 '{poolingObjectName}'키가 존재합니다!");
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

        if (poolIndexDictionary.ContainsKey(objectName)) //해당 키가 있으면 = 해당 오브젝트가 이미 풀링되어 있으면
        {
            Debug.Log($"이미 동일한 키가 존재합니다!");
            return;
        }
        CreateObjectPools(_objectToPooling, _poolingCount, _customKeyName);
    }

    public GameObject GetObject(string _objectName)
    {
        if (!poolIndexDictionary.ContainsKey(_objectName))
        {
            Debug.LogWarning($"'{_objectName}'오브젝트를 풀에서 찾지 못하였습니다.");
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
