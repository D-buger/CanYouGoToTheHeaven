using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class ImageLoader
{
    private const string FILE_PATH = "Assets/";

    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public ImageLoader(string spritePath)
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { SOO.Util.StringBuilder() });
        for (int i = 0; i < guids.Length; i++) {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guids[i]));
            sprites.Add(sprite.name, sprite);
        }
    }
}
