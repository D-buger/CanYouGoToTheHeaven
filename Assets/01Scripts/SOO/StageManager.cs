using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private Slider remainingWater;

    private PlayerStats stat;

    private void Awake()
    {
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
