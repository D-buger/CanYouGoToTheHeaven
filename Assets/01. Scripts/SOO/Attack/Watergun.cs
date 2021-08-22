using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    public int maxWaterAmount = 100;
    [SerializeField]
    private int waterAmount;
    public int WaterAmount
    {
        get
        {
            return waterAmount;
        }
        set
        {
            waterAmount = Mathf.Min(value, maxWaterAmount);
            waterAmount = Mathf.Max(waterAmount, 0);
        }
    }

    public int damage = 1;

    Water nowWater;

    Vector2 angle => SOO.Util.AngleToVector(transform.rotation.eulerAngles.z);

    private void Awake()
    {
        waterAmount = maxWaterAmount;
    }

    public void Update()
    {
        if (GameManager.Instance.input.behaviourActive && waterAmount > 0)
        {
            WaterSet();
        }
        else
        {
            if (null != nowWater)
                nowWater = null;
        }
    }

    private void WaterSet()
    {
        if (nowWater == null)
        {
            GameObject obj = ObjectPoolManager.Inst.pool.Pop();
            nowWater = obj.GetComponent<Water>();
            nowWater.SetFirst(transform.position, angle, damage);
        }
        else
        {
            if (nowWater.VertexSet(transform.position, angle))
                waterAmount -= 1;
        }
    }
}
