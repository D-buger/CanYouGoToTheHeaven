using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunTest : MonoBehaviour
{
    Transform playerPosition;

    WaterTest nowWater;

    [SerializeField]
    private float waterDotSec;

    private void Update()
    {
        if (GameManager.Instance.input.behaviourActive)
        {
            if (nowWater == null)
            {
                GameObject obj = ObjectPoolManager.Inst.pool.Pop();
                nowWater = obj.GetComponent<WaterTest>();
            }
            else
            {
                nowWater.UpdateWater();
            }
        }
        else
        {
            if (nowWater != null)
                nowWater = null;
        }
        
    }

}
