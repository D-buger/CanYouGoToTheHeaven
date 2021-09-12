using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FileLoader<T>
    where T : Object
{
    private const string FILE_PATH = "Assets/";

    public Dictionary<string, T> files = new Dictionary<string, T>();

    private System.Type typeOfT = typeof(T);

    private string filePath;
    private string typeOfString;

    public FileLoader(string path, string type) : this(path)
        => typeOfString = type;

    public FileLoader(string path)
     => SetFileLoader(filePath = path);

    public void SetFileLoader(string spritePath)
    {
        string[] guids = AssetDatabase.FindAssets($"t:" +
            $"{typeOfString ?? StringFormat(typeOfT.ToString())}", 
            new[] { SOO.Util.StringBuilder(FILE_PATH, spritePath) });
        for (int i = 0; i < guids.Length; i++) {
            T file = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
            files.Add(file.name, file);
        }
    }

    private string StringFormat(string str) 
        => str.Contains("UnityEngine.") ? str.Remove(0, 12) : str;

    public T GetFile(string name) 
        => files[name];
}
