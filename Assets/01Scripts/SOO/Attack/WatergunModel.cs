using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public static event Action<int> maxWaterAmountCallback;
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
            maxWaterAmountCallback?.Invoke(maxWaterAmount);
        }
    }

    public static event Action<int> currentWaterAmountCallback;
    private int waterAmount;
    public int WaterAmount
    {
        get
        {
            return waterAmount;
        }
        set
        {
            waterAmount = Mathf.Clamp(value, 0, maxWaterAmount);
            currentWaterAmountCallback?.Invoke(waterAmount);
        }
    }

    public int Damage { get; set; } = 1;
    public Water NowWater { get; set; }
}