using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI 분리 안함
public class StageManager : SingletonBehavior<StageManager>
{
    public ItemManager Item { get; private set; }

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    public StageGenerator StageGenerator { get; private set; }

    [SerializeField]
    private Slider remainingWater;

    protected override void OnAwake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;

        StageGenerator = GameObject.FindObjectOfType<StageGenerator>();
        Item = new ItemManager();
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
