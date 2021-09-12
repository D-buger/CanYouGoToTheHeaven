using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemModel : MonoBehaviour
{
    private SpriteRenderer renderer;

    [SerializeField]
    protected Sprite ItemImage { get; private set; }
    protected int option;

    public int price;
    public string effect;

    //monobehaviour�� ����ϸ� �����ڸ� ����� �� ���⶧���� ����� �Լ�
    public void SetFirst(int option, int price, string effect)
    {
        this.option = option;
        this.price = price;
        this.effect = effect;
    }

    private void OnEnable()
    {
        if (renderer == null)
            renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ItemImageSet(Sprite sprite)
    {
        ItemImage = sprite;
        renderer.sprite = ItemImage;
    }

    protected abstract void ItemEffect(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("������ �ߵ�");
            ItemEffect(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
