using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackImageSet : MonoBehaviour
{
    private SpriteRenderer backImageRenderer;

    private List<Dictionary<string, string>> backImageSet;

    private FileLoader<Sprite> backImages;

    private void Awake()
    {
        backImageRenderer = GetComponent<SpriteRenderer>();
        backImageSet = CSVReader.Read("BackImage");
        backImages = new FileLoader<Sprite>("Images/Background");

        StageManager.changeCurrentRoom += ChangeBackImage;
    }

    string[] fileName;
    private void ChangeBackImage(int currentRoom)
    {
        fileName = backImageSet[currentRoom / (StageManager.Instance.MapManager.levelInStage + 1)]["BackImage"].Split(',');
        if(fileName.Length == 1)
        {
            backImageRenderer.sprite = backImages.GetFile(fileName[0]);
        }
        else
        {
            backImageRenderer.sprite = backImages.GetFile(fileName[Random.Range(0, fileName.Length)]);
        }
    }

    
}
