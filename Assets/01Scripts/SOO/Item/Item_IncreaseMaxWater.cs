using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_IncreaseMaxWater : ItemModel
{
    protected override Sprite ItemImageSet() =>
        GameManager.Instance.image.sprites.TryGetValue("pluscan", out Sprite sprite) ? sprite : null;

    protected override void ItemEffect(GameObject player)
    {
        player.GetComponent<Player>().watergun.maxWaterAmount += 10;
    }
}
