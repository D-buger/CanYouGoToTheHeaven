using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunModel
{
    public WaterGunModel(int max)
    {
        MaxWaterAmount = max;
        WaterAmount = MaxWaterAmount;
    }

    public WaterGunModel(int max, int damage) : this(max)
    {
        Damage = damage;
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
            //StageManager.Instance.UiManager.SetMaxWater(maxWaterAmount);

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
            Mathf.Clamp(value, 0, maxWaterAmount);
            //StageManager.Instance.UiManager.SetWater(waterAmount);
        }
    }

    public int Damage { get; set; } = 1;
    public Water NowWater { get; set; }
}