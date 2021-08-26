using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_MonsterSpawnManager : MonoBehaviour
{
    public static HTest_MonsterSpawnManager instance = null;

    // Start is called before the first frame update
    private void Awake() //Monster List
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {

    }
}
