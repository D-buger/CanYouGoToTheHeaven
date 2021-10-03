using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Vector2 ShopPosition { get; private set; }
    public Vector2 DoorPosition { get; private set; }
    
    private ItemShelve[] itemShelves;

    private TMP_Text text;
    private TMP_Text priceText;
    
    private readonly string puchaseErrorText = "보유 재화가 부족합니다.";
    private bool errorTextEnabled = false;
    private Timer errorTimer;

    private int shelvesCount;

    private void Awake()
    {
        Transform itemShelvesParent = transform.GetChild(0);
        shelvesCount = itemShelvesParent.childCount - 1;
        itemShelves = new ItemShelve[shelvesCount];
        for (int i = 0; i < shelvesCount; i++)
        {
            itemShelves[i] = itemShelvesParent.GetChild(i).GetComponent<ItemShelve>();
        }
        priceText = itemShelvesParent.GetChild(shelvesCount).GetComponent<TMP_Text>();

        ShopPosition = transform.position;
        DoorPosition = transform.GetChild(3).transform.position;

        text = transform.GetChild(2).GetComponent<TMP_Text>();
        
        
    }

    private Transform playerPosition => StageManager.Instance.Player.transform;

    private void Update()
    {
        if (!StageManager.PlayerInStage)
        {
            if (!errorTextEnabled)
                SetExplanation(PlayerPositionCheck());
            else
                ErrorText();
        }
    }

    private int PlayerPositionCheck()
    {
        float x = playerPosition.position.x;
        //FIX : 플레이어가 상점에 왔을 때 번호를 부여하고 입력 키에 따라 위치를 확인하고 번호를 변경한다.
        for (int i = 0; i < shelvesCount; i++)
        {
            if (x >= itemShelves[i].transform.position.x - StageManager.Instance.ItemManager.ItemRadius
             && x <= itemShelves[i].transform.position.x + StageManager.Instance.ItemManager.ItemRadius)
            {
                return i;
            }
        }
        return -1;
    }

    private void ErrorText()
    {
        if (errorTimer.TimerUpdate())
        {
            text.color = Color.white;
            errorTextEnabled = false;
            return;
        }

        text.text = puchaseErrorText;
        text.color = Color.red;
    }

    private void SetExplanation(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < shelvesCount
            && itemShelves[itemIndex].Item != null)
        {
            text.text = itemShelves[itemIndex]?.Item.Effect;
            priceText.transform.position = 
                new Vector3(itemShelves[itemIndex].transform.position.x, priceText.transform.position.y );
            priceText.text = itemShelves[itemIndex]?.Item.Price.ToString();

            if (GameManager.Instance.input.BehaviourActive)
                BuyItem(itemIndex);
        }
        else
            text.text = priceText.text = "";
    }

    private void BuyItem(int itemIndex)
    {
        if(StageManager.Instance.Stat.Soul >= itemShelves[itemIndex]?.Item.Price)
        {
            if (!StageManager.data.isBuyInShop)
                StageManager.data.isBuyInShop = true;
            StageManager.Instance.Stat.Soul -= itemShelves[itemIndex].Item.Price;
            itemShelves[itemIndex].CanBuy = true;
            itemShelves[itemIndex].gameObject.SetActive(false);
        }
        else{
            priceText.text = "";
            errorTimer = new Timer(0.8f);
            errorTextEnabled = true;
        }
    }

    private void OnEnable()
    {
        for(int i = 0;i < itemShelves.Length; i++)
        {
            itemShelves[i].gameObject.SetActive(true);
            itemShelves[i].CanBuy = false;
        }
    }
}
