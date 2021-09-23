using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Vector2 ShopPosition { get; private set; }
    public Vector2 DoorPosition { get; private set; }
    
    private Transform[] itemShelves;
    private ItemModel[] items;
    private ItemManager itemManager;

    private TMP_Text text;
    private TMP_Text priceText;

    private string doorText = "스테이지로 돌아갑니다.";
    private string refillmuchineText = "음료를 1회 최대치로 충전합니다.";
    private string puchaseErrorText = "보유 재화가 부족합니다.";

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
        float x = playerPosition.position.x;
        //FIX : 플레이어가 상점에 왔을 때 번호를 부여하고 입력 키에 따라 위치를 확인하고 번호를 변경한다.
        for (int i = 0; i < shelvesCount; i++)
        {
            if (x >= itemShelves[i].position.x - itemManager.ItemRadius
             && x <= itemShelves[i].position.x + itemManager.ItemRadius)
            {
                return i;
            }
        }
        if (x >= DoorPosition.x)
            return shelvesCount;
        else if (x >= DoorPosition.x)
            return shelvesCount + 1; //리필머신
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
        else if (itemIndex == shelvesCount)
        {
            text.text = doorText;
            priceText.text = "";
        }
        else if (itemIndex == shelvesCount + 1)
        {
            text.text = refillmuchineText;
            priceText.text = "";
        }
        else
            text.text = priceText.text = "";
    }

    private void BuyItem(int itemIndex)
    {

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
