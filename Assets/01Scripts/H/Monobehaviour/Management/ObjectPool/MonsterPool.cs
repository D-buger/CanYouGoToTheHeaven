using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    Queue<GameObject> monsterQueue = new Queue<GameObject>();

    GameObject pooledObject; //풀링된(본인이 담당하는) 오브젝트

    public void StartingInitialize(GameObject _monster, int _poolingCount)
    {
        pooledObject = _monster;
        for (int i = 0; i < _poolingCount; i++)
        {
            CreatingObject();
        }
    }

    void CreatingObject()
    {
        GameObject obj = Instantiate(pooledObject, transform);
        obj.name = pooledObject.name;
        monsterQueue.Enqueue(obj);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }

    public GameObject GetObject()
    {
        GameObject obj;

        if (monsterQueue.Count > 0)
        {
            obj = monsterQueue.Dequeue();
        }
        else
        {
            CreatingObject();
            obj = monsterQueue.Dequeue();
        }
        obj.transform.SetParent(null);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
        _gameObject.transform.SetParent(transform);
        monsterQueue.Enqueue(_gameObject);
    }
}
