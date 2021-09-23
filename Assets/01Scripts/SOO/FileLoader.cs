using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader<T>
    where T : Object
{
    public Dictionary<string, T> files = new Dictionary<string, T>();

    private string filePath;
    private string typeOfString;

    public FileLoader(string path, string type) : this(path)
        => typeOfString = type;

    public FileLoader(string path)
     => SetFileLoader(filePath = path);

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

#else
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
#endif

    private string StringFormat(string str)
        => str.Contains("UnityEngine.") ? str.Remove(0, 12) : str;

    public T GetFile(string name) 
        => files[name];
}
