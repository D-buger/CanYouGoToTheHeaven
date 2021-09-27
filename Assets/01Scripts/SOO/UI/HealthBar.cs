using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private Texture2D texture;

    private FileLoader<Texture2D> hudImage;
    private Rect frameRect;
    private int ratio;

    private Texture2D emptyHeart;
    private Texture2D fullHeart;
    private Texture2D halfHeart;

    private void Awake()
    {
        hudImage = new FileLoader<Texture2D>("Images/UI", "UI_");
        texture = new Texture2D(0, 0);
        texture.filterMode = FilterMode.Point;

        image = GetComponent<Image>();
        frameRect = image.rectTransform.rect;
        ratio = (int)(frameRect.width / frameRect.height);

        emptyHeart = hudImage.GetFile("EmptyLife");
        fullHeart = hudImage.GetFile("Life");
        MakeHalfHeart();

        maxShowSprite = ratio;
        textureWidth = emptyHeart.width * ratio;
        textureHeight = emptyHeart.height;
    }
    
    //하트 반 칸 + 빈 하트 반칸으로 반하트 미리 만들어두기
    private void MakeHalfHeart()
    {
        halfHeart = new Texture2D(emptyHeart.width, emptyHeart.height);
        halfHeart.filterMode = FilterMode.Point;

        halfHeart.SetPixels(0, 0, halfHeart.width / 2, halfHeart.height,
            fullHeart.GetPixels(0, 0, fullHeart.width / 2, fullHeart.height));

        halfHeart.SetPixels(halfHeart.width / 2, 0, halfHeart.width / 2, halfHeart.height,
            emptyHeart.GetPixels(emptyHeart.width / 2, 0, emptyHeart.width / 2, emptyHeart.height));

        halfHeart.Apply();
    }

    int maxShowSprite;

    int textureWidth;
    int textureHeight;
    
    /// <summary>
    /// 목숨 최대치 변경시 텍스처 크기 변경
    /// </summary>
    /// <param name="maxHp">최대 목숨</param>
    /// <param name="nowHp">현재 목숨</param>
    public void SizeChange(int maxHp, int nowHp)
    {
        int HP = (int)(maxHp * 0.5f);

        maxShowSprite = (textureWidth / emptyHeart.width) * (textureHeight / emptyHeart.height);
        textureWidth = maxShowSprite > HP ?
            textureWidth :
            emptyHeart.width * HP;
        textureHeight = maxShowSprite > HP ?
            textureHeight :
            emptyHeart.height +
            emptyHeart.height * (HP <= ratio ? 0 : HP - ratio) / ratio;

        if (texture == null)
            texture = new Texture2D(textureWidth, textureHeight);
        else
            texture.Resize(textureWidth, textureHeight);

        Color[] color = texture.GetPixels();
        for (int i = 0; i < color.Length; i++)
            color[i] = new Color(0, 0, 0, 0);
        texture.SetPixels(color);

        Vector2 pivot = new Vector2(
            texture.width / 2,
            texture.height / 2);
        Rect rect = new Rect(0, 0, texture.width, texture.height);

        Sprite lifeSprite = Sprite.Create(texture, rect, pivot);
        image.sprite = lifeSprite;

        SetInsideGraphic(maxHp, nowHp);
    }
    
    /// <summary>
    /// 이미 정해진 텍스처 크기 내에서 목숨의 그래픽들만 변경한다.
    /// </summary>
    /// <param name="maxHp">최대 목숨</param>
    /// <param name="nowHp">현재 목숨</param>
    public void SetInsideGraphic(int maxHp, int nowHp)
    {
        int width = 0, height = 0;
        for(int i = 0;i < maxHp - 1; i += 2)
        {
            width = (i / 2) % (int)(textureWidth / fullHeart.width) * fullHeart.width;
            height += fullHeart.height * (width == 0 ? 1 : 0);

            if (i < nowHp - 1)
            {
                texture.SetPixels(width, texture.height - height,
                    fullHeart.width, fullHeart.height, fullHeart.GetPixels());
            }
            else if(nowHp % 2 != 0 && i < nowHp)
            {
                texture.SetPixels(width, texture.height - height, 
                    halfHeart.width, halfHeart.height, halfHeart.GetPixels());
            }
            else if(i >= nowHp)
            {
                texture.SetPixels(width, texture.height - height,
                    emptyHeart.width, emptyHeart.height, emptyHeart.GetPixels());
            } 
        }

        texture.Apply();
    }
}