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

    private void Update()
    {
        if (GameManager.Instance.input.behaviourActive)
        {
            ShootWatergun();
        }
        else
        {
            if (null != nowWater)
                nowWater = null;
        }
    }

    public void ShootWatergun()
    {
        if (waterAmount > 0)
        {
            WaterSet();
        }
    }

    private void WaterSet()
    {
        if (nowWater == null)
        {
            GameObject obj = ObjectPoolManager.Inst.pool.Pop();
            nowWater = obj.GetComponent<Water>();
            nowWater.SetFirst(damage);
        }

        if (nowWater.VertexSet(transform.position, angle, TimeSet()))
           waterAmount -= 1;
    }

    private float TimeSet()
    {
        if (maxWaterAmount / 100 * 20 > waterAmount)
        {
            return 0.3f;
        }
        else if (maxWaterAmount / 100 * 50 > waterAmount)
        {
            return 0.5f;
        }
        else if(maxWaterAmount / 100 * 50 <= waterAmount)
        {
            return 1f;
        }
        else
        {
            Debug.Log("해당 되지 않는 양");
            return 0;
        }
    }
}
