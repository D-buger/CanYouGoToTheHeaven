using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImageLoader
{
    private const string SPRITE_PATH = "Assets/02.Sprites/";

    private void Awake()
    {
        AssetDatabase.LoadAssetAtPath<Sprite>(SOO.Util.StringBuilder(SPRITE_PATH, ""));
    }
}
