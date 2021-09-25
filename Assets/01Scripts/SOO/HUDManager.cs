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

    private Image image;

    [SerializeField]
    private int maxHP = 6;
    [SerializeField]
    private int sampleHp = 6;

    [SerializeField]
    Texture2D texture;

    public void Awake()
    {
        hudImage = new FileLoader<Texture2D>("Images/UI", "UI_");
        image = GetComponent<Image>();
        SetTexture();
    }

    [ContextMenu("SetTexture")]
    public void SetTexture()
    {
        Rect frameRect = image.rectTransform.rect;
        int ratio =  (int)(frameRect.width / frameRect.height);

        Texture2D lifeTexture = hudImage.GetFile("EmptyLife");

        int testMul = (int)(maxHP / (2 * ratio + 1) + 1);
        int width = lifeTexture.width * (ratio + Mathf.Max(0, (maxHP / 2 - ratio)));
        int height = lifeTexture.height + 
            lifeTexture.height * (maxHP / 2 <= ratio ? 0 : maxHP / 2 - ratio) / ratio;
        int heightCount = height / lifeTexture.height;

        Texture2D testTexture = new Texture2D(
            width,
            height);

        testTexture.filterMode = FilterMode.Point;

        for(int i = 0; i < maxHP * 0.5f; i++)
        {
            testTexture.SetPixels(
                0 + i * lifeTexture.width,
                testTexture.height - (height / lifeTexture.height ) * lifeTexture.height,
                lifeTexture.width, lifeTexture.height, lifeTexture.GetPixels());
        }
        testTexture.Apply();

        Vector2 pivot = new Vector2(
            testTexture.width / 2,
            testTexture.height / 2);
        Rect rect = new Rect(
            0,
            0,
            testTexture.width,
            testTexture.height);

        lifeSprite = Sprite.Create(testTexture, rect, pivot);
        image.sprite = lifeSprite;
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
