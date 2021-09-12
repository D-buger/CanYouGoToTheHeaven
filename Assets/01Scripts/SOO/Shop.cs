using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private float CheckDistance = 2f;
    
    private Transform[] itemShelves;
    private GameObject[] items;
    private ItemManager itemManager;

    private TMP_Text text;

    private int shelvesCount;

    private void Awake()
    {
        itemManager = StageManager.Instance.Item;

        Transform itemShelvesParent = transform.GetChild(0);
        shelvesCount = itemShelvesParent.childCount;

        itemShelves = new Transform[shelvesCount];
        items = new GameObject[shelvesCount];
        for (int i = 0; i < shelvesCount; i++)
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
