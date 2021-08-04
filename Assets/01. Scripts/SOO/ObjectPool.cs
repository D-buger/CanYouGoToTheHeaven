using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolObj;
    [SerializeField, Min(0)]
    private int allocateCount;

    private Stack<GameObject> poolList = new Stack<GameObject>();

    private void Start()
    {
        Allocate();
    }

    public void Allocate()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            GameObject obj = Instantiate(poolObj, transform);
            obj.SetActive(false);
            poolList.Push(obj);
        }
    }

    public GameObject Pop()
    {
        if (poolList.Count != 0)
        {
            GameObject obj = poolList.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Debug.Log("스택이 비어있습니다.");
            return null;
        }
    }

    public void Push(GameObject _obj)
    {
        _obj.SetActive(false);
        poolList.Push(_obj);
    }
}
