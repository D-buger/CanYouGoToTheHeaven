using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BlueHeart : ItemModel
{
    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.MaxHP += option;
    }
}
