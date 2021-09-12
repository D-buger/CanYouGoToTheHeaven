using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_GameManeger : MonoBehaviour
{
    public static HTest_GameManeger instance = null;

    // Start is called before the first frame update
    void Awake()
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
}
