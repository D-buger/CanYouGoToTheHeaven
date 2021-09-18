using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private float CheckDistance = 2f;

    public Vector2 ShopPosition { get; private set; }
    public Vector2 DoorPosition { get; private set; }
    
    private Transform[] itemShelves;
    private ItemModel[] items;
    private ItemManager itemManager;

    private TMP_Text text;
    private TMP_Text priceText;

    private int shelvesCount;

    private void Awake()
    {
        itemManager = StageManager.Instance.ItemManager;

        Transform itemShelvesParent = transform.GetChild(0);
        shelvesCount = itemShelvesParent.childCount - 1;

        ShopPosition = transform.position;
        DoorPosition = transform.GetChild(2).transform.position;

        text = transform.GetChild(3).GetComponent<TMP_Text>();

        itemShelves = new Transform[shelvesCount];
        items = new ItemModel[shelvesCount];
        for (int i = 0; i < shelvesCount; i++)
        {
            itemShelves[i] = itemShelvesParent.GetChild(i);
        }
        priceText = itemShelvesParent.GetChild(shelvesCount).GetComponent<TMP_Text>();
    }

    private Transform playerPosition => StageManager.Instance.Player.transform;

    private void Update()
    {
        if (!StageManager.Instance.PlayerInStage)
        {
            SetExplanation(PlayerPositionCheck());
        }
    }

    private int PlayerPositionCheck()
    {
        for (int i = 0; i < shelvesCount; i++)
        {
            if (playerPosition.position.x >= itemShelves[i].position.x - itemManager.ItemRadius
             && playerPosition.position.x <= itemShelves[i].position.x + itemManager.ItemRadius)
            {
                return i;
            }
        }
        return -1;
    }

    private void SetExplanation(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < shelvesCount)
        {
            text.text = items[itemIndex]?.Effect;
            priceText.transform.position = 
                new Vector3(items[itemIndex].transform.position.x, priceText.transform.position.y );
            priceText.text = items[itemIndex]?.Price.ToString();
        }
        else
            text.text = priceText.text = "";
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
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = itemManager.GetRandomItem();
            items[i].GetComponent<Collider2D>().enabled = false;
            items[i].transform.position = itemShelves[i].position;
        }
    }

    private void ShelvesClear()
    {
        itemManager.SetItem(items);
    }
}
