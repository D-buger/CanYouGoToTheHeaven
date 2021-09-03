using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BlueHeart : ItemModel
{
    protected override Sprite ItemImageSet() =>
        GameManager.Instance.image.sprites.TryGetValue("blueheart", out Sprite sprite) ? sprite : null;

    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().stats.MaxHP++;
    }
}
