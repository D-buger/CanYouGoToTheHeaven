using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Dictionary<string, string>> itemInfo;

    public ImageLoader image;

    public List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        itemInfo = CSVReader.Read("ItemInfo");

    }
}
