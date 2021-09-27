using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader<T>
    where T : Object
{
    public Dictionary<string, T> files = new Dictionary<string, T>();

    private string filePath;
    private string typeOfString;

    public FileLoader(string path, string divideString)
        => SetFileLoader(path, divideString);

    public FileLoader(string path)
     => SetFileLoader(filePath = path);

    public FileLoader<T> SetType(string type)
    {
        typeOfString = type;
        return this;
    }

    //TODO : 출시 직전에 #if문 두개에 있는 SetFileLoader 각각 내용 바꾸기
#if UNITY_EDITOR
    private readonly string FILE_PATH = Application.streamingAssetsPath;

    private System.Type typeOfT = typeof(T);

    public void SetFileLoader(string filePath)
    {
        T[] loadedFile = Resources.LoadAll<T>(filePath);
        for (int i = 0; i < loadedFile.Length; i++)
        {
            files.Add(loadedFile[i].name, loadedFile[i]);
        }
    }
    public void SetFileLoader(string filePath, string divideString)
    {
        T[] loadedFile = Resources.LoadAll<T>(filePath);
        for (int i = 0; i < loadedFile.Length; i++)
        {
            files.Add(StringFormat(loadedFile[i].name, divideString), loadedFile[i]);
        }
    }

#else
    private readonly string FILE_PATH = Application.streamingAssetsPath;

    private System.Type typeOfT = typeof(T);

    public void SetFileLoader(string filePath)
    {
        T[] loadedFile = Resources.LoadAll<T>(filePath);
        for (int i = 0; i < loadedFile.Length; i++)
        {
            files.Add(loadedFile[i].name, loadedFile[i]);
        }
    }
    public void SetFileLoader(string filePath, string divideString)
    {
        T[] loadedFile = Resources.LoadAll<T>(filePath);
        for (int i = 0; i < loadedFile.Length; i++)
        {
            files.Add(StringFormat(loadedFile[i].name, divideString), loadedFile[i]);
        }
    }
#endif

    private string StringFormat(string str, string divide)
    {
        return str.Contains(divide) ? str.Remove(0, divide.Length) : str;
    }

    public T GetFile(string name) 
        => files[name];
}

/*
    public void SetFileLoader(string spritePath)
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:" +
                $"{typeOfString ?? StringFormat(typeOfT.ToString())}",
                new[] { SOO.Util.StringBuilder(FILE_PATH, filePath) });
        for (int i = 0; i < guids.Length; i++)
        {
            T file = UnityEditor.AssetDatabase.LoadAssetAtPath<T>
                (UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]));
            files.Add(file.name, file);
        }
        
    }

    */