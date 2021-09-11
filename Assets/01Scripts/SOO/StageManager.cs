using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : SingletonBehavior<StageManager>
{
    public ItemManager item { get; private set; }

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    [SerializeField]
    private Slider remainingWater;

    protected override void OnAwake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;

        item = new ItemManager();
    }

    private void Update()
    {
    }

    public void SetMaxWater(float maxValue)
    {
        remainingWater.maxValue = maxValue;
    }

    public void SetWater(float value)
    {
        remainingWater.value = value;
    }
}
