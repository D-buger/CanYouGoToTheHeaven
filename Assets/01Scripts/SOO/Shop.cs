using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject[] itemShelves;
    
    private void Awake()
    {
        Transform itemShelvesParent = transform.GetChild(0);
        itemShelves = new GameObject[itemShelvesParent.childCount];
        for (int i = 0; i < itemShelves.Length; i++)
        {
            itemShelves[i] = itemShelvesParent.GetChild(i).gameObject;
        }
    }


}
