using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunModel
{
    public WaterGunModel(int max)
    {
        maxWaterAmount = max;
        waterAmount = maxWaterAmount;
    }

    private int maxWaterAmount;
    public int MaxWaterAmount
    {
        get
        {
            return maxWaterAmount;
        }
        set
        {
            maxWaterAmount = value;
            StageManager.Instance.SetMaxWater(maxWaterAmount);

        }
    }

    private int waterAmount;
    public int WaterAmount
    {
        get
        {
            return waterAmount;
        }
        set
        {
            waterAmount = Mathf.Min(value, MaxWaterAmount);
            waterAmount = Mathf.Max(waterAmount, 0);
            StageManager.Instance.SetWater(waterAmount);
        }
    }

    public int Damage { get; set; } = 1;
    public Water NowWater { get; set; }
}