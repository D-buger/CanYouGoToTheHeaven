using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RedHeart : ItemModel
{
    [SerializeField, Min(0)]
    private int HP;

    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.CurrentHP += HP;
    }
}
