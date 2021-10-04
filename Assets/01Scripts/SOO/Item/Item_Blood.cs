using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Blood : ItemModel
{
    int previousValue = 0;

    protected override void ItemEffect(GameObject player)
    {
        previousValue = StageManager.data.MonsterKill;
        player.GetComponent<Player>().playerBuffs += Mosquito;
    }

    private void Mosquito()
    {
        if(previousValue >= StageManager.data.MonsterKill + option)
        {
            previousValue = StageManager.data.MonsterKill;
            StageManager.Instance.Stat.CurrentHp++;
        }
    }
}
