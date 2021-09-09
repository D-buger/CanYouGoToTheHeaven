using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    private Transform[] itemShelves;
    private GameObject[] items;
    private ItemManager itemManager;

    private TMP_Text text;

    private int count;

    private void Awake()
    {
        itemManager = StageManager.Instance.item;

        Transform itemShelvesParent = transform.GetChild(0);
        count = itemShelvesParent.childCount;

        itemShelves = new Transform[count];
        items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            itemShelves[i] = itemShelvesParent.GetChild(i);
        }
    }

    private void OnEnable()
    {
        ShelvesReload();
    }

    private void OnDisable()
    {
        ShelvesClear();
    }

    private void ShelvesReload()
    {
        for(int i = 0; i < itemShelves.Length; i++)
        {
            items[i] = itemManager.GetRandomItem();
            items[i].GetComponent<CircleCollider2D>().enabled = false;
            items[i].transform.position = itemShelves[i].position;
        }
    }

    private void ShelvesClear()
    {
        itemManager.SetItem(items);
    }
}
