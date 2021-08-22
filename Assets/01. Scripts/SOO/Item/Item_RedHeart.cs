using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RedHeart : ItemModel
{
    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.CurrentHP += 1;
    }
}
