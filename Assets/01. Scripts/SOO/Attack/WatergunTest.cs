using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunTest : MonoBehaviour
{
    [SerializeField]
    private int maxWaterAmount = 100;

    private int waterAmount;
    public int WaterAmount => waterAmount;

    WaterTest nowWater;

    Vector2 angle => SOO.Util.AngleToVector(transform.rotation.eulerAngles.z);

    public void Update()
    {
        if (GameManager.Instance.input.behaviourActive)
        {
            if (nowWater == null)
            {
                GameObject obj = ObjectPoolManager.Inst.pool.Pop();
                nowWater = obj.GetComponent<WaterTest>();
                nowWater.SetFirst(transform.position, angle);
            }
            else
            {
                nowWater.UpdateWater(transform.position, angle);
            }
        }
        else
        {
            if (null != nowWater)
                nowWater = null;
        }
    }
}
