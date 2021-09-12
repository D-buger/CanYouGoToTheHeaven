using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_IncreaseMaxWater : ItemModel
{
    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.watergun.Model.MaxWaterAmount += option;
    }
}
