using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemModel : MonoBehaviour
{
    private SpriteRenderer renderer;

    private Sprite itemImage;
    protected int option;

    public int Price { get; private set; }
    public string Effect { get; private set; }

    //monobehaviour�� ����ϸ� �����ڸ� ����� �� ���⶧���� ����� �Լ�
    public void SetFirst(int option, int price, string effect)
    {
        this.option = option;
        this.Price = price;
        this.Effect = effect;
    }

    private void OnEnable()
    {
        if (renderer == null)
            renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ItemImageSet(Sprite sprite)
    {
        itemImage = sprite;
        renderer.sprite = itemImage;
    }

    protected abstract void ItemEffect(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StageManager.data.GottenItemCount++;
            ItemEffect(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
