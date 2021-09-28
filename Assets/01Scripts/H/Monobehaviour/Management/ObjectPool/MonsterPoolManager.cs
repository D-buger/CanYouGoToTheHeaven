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

    Dictionary<string, int> poolIndexDictionary = new Dictionary<string, int>(); //string을 받으면 해당 이름을 가진 오브젝트가 몇 번째 풀에 있는지
    MonsterPool[] monsterPoolComponents; //각 풀들의 컴포넌트 캐싱용

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

    void AddPoolingObject() //CSV에서 읽어온 정보를 토대로 몬스터들을 할당시켜주는 역할
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
            Debug.LogWarning($"{gameObject.name}: 오브젝트 풀 프리팹이 할당되지 않았습니다");
        }
        monsterPoolComponents = new MonsterPool[objectList.Count];
        CreateObjectPools();
    }
}
