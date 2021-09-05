using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<Dictionary<string, string>> itemInfo;

    private ImageLoader image;

    [SerializeField]
    private GameObject itemModel;
    public List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        image = new ImageLoader("02Sprites/item");
        itemInfo = CSVReader.Read("ItemInfo", out int size);
        size -= 2;

        int option, price;

        for(int i = 0; i < size; i++)
        {
            GameObject ins = Instantiate(itemModel, transform);
            ins.AddComponent(System.Type.GetType(itemInfo[i]["ItemID"]));
            ItemModel model = ins.GetComponent<ItemModel>();
            if (model)
            {
                int.TryParse(itemInfo[i]["Option"], out option);
                int.TryParse(itemInfo[i]["ItemPrice"], out price);
                model.SetFirst(option, price, itemInfo[i]["Effective"]);
                model.ItemImageSet(image.GetImage(itemInfo[i]["Icon"]));
            }
            ins.SetActive(false);
        }
    }
    
}
