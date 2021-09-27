using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Canvas canvas;

    private Slider remainingWater;
    private FileLoader<Texture2D> hudImage;

    private Sprite lifeSprite;
    
    public void Awake()
    {
        hudImage = new FileLoader<Texture2D>("Images/UI", "UI_");
    }

    public void SetMaxWater(float maxValue)
    {
        remainingWater.maxValue = maxValue;
    }

    public void SetWater(float value)
    {
        remainingWater.value = value;
    }

}
