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

    //monobehaviour를 상속하면 생성자를 사용할 수 없기때문에 대신할 함수
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
            Debug.Log("아이템 발동");
            ItemEffect(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
