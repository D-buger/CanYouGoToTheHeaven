using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_MonsterSpawnManager : MonoBehaviour
{
    public static HTest_MonsterSpawnManager instance = null;

    // Start is called before the first frame update
    void Start()
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

    public Dictionary<int, GameObject> monsterList_Grade;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            foreach (var monster in monsterList_Grade)
            {
                Debug.Log($"{monster.Key}: {monster.Value}");
            }
            Debug.Log("몬스터 출력 완료");
        }
    }
}
