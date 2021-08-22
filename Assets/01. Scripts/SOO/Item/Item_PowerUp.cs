using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PowerUp : ItemModel
{
    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().watergun.damage = 2;
    }
}
