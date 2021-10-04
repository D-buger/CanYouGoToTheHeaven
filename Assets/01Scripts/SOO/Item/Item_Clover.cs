using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Clover : ItemModel
{
    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.chanceOfSpawnGoldenMonster += option;
    }
}
