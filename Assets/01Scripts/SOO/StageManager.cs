using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : SingletonBehavior<StageManager>
{
    public ItemManager item;

    [SerializeField]
    private Slider remainingWater;

    private PlayerStats stat;


    protected override void OnAwake()
    {
        item = new ItemManager();
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stats;
    }

    private void Update()
    {
        SetWater();
    }

    public void SetWater()
    {
        remainingWater.maxValue = stat.watergun.maxWaterAmount;
        remainingWater.value = stat.watergun.WaterAmount;
    }
}
