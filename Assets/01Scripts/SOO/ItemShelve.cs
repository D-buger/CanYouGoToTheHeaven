using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShelve : MonoBehaviour
{
    public ItemModel Item { get; private set; }
    public bool CanBuy
    {
        set
        {
            Item.GetComponent<Collider2D>().enabled = value;
        }
    }

    private void OnEnable()
    {
        if(StageManager.Instance.ItemManager != null)
            ShelveReload();
    }

    private void OnDisable()
    {
        if (StageManager.Instance.ItemManager != null)
            ShelveClear();
    }

    private void ShelveReload()
    {
        Item = StageManager.Instance.ItemManager.GetRandomItem();
        Item.transform.position = transform.position;
    }

    private void ShelveClear()
    {
        if(Item != null)
            StageManager.Instance.ItemManager.SetItem(Item);
    }
}
