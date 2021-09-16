using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public List<ItemModel> items = new List<ItemModel>();
    public float ItemRadius { get; private set; }

    private List<Dictionary<string, string>> itemInfo;

    private FileLoader<Sprite> image;
    private GameObject itemParent;
    
    private GameObject itemModel;
    private int size;

    public ItemManager()
    {
        image = new FileLoader<Sprite>("02Sprites/item");
        itemModel = new FileLoader<GameObject>("04Prefabs", "Prefab").GetFile("ItemDummy");
        itemParent = new GameObject();
        itemParent.name = "ItemParent";

        ItemRadius = itemModel.GetComponent<CircleCollider2D>().radius * itemModel.transform.localScale.x;
        itemInfo = CSVReader.Read("ItemInfo", out size);
        size -= 2;

        CreateItems();
    }

    public void CreateItems()
    {
        int option, price;

        for (int i = 0; i < size; i++)
        {
            GameObject ins = GameObject.Instantiate(itemModel, itemParent.transform);
            ins.name = itemInfo[i]["ItemName"];
            ins.AddComponent(System.Type.GetType(itemInfo[i]["ItemID"]));
            ItemModel model = ins.GetComponent<ItemModel>();
            if (model)
            {
                int.TryParse(itemInfo[i]["Option"], out option);
                int.TryParse(itemInfo[i]["ItemPrice"], out price);
                model.SetFirst(option, price, itemInfo[i]["Effective"]);
                model.ItemImageSet(image.GetFile(itemInfo[i]["Icon"]));
            }
            items.Add(model);
            ins.SetActive(false);
        }
    }

    public ItemModel GetRandomItem()
    {
        ItemModel randomItem = items[Random.Range(0, items.Count)];
        items.Remove(randomItem);
        randomItem?.gameObject.SetActive(true);
        return randomItem ?? default(ItemModel);
    }

    public void SetItem(params ItemModel[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = Vector3.zero;
            items.Add(obj[i]);
            obj[i]?.gameObject.SetActive(false);
        }
    }
}
