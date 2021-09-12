using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager inst;
    public static ObjectPoolManager Inst
    {
        get
        {
            return inst;
        }
    }

    public ObjectPool pool;

    private void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
            return;
        }
        inst = this;
    }
}
