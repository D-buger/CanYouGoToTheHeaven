using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemModel : MonoBehaviour
{
    private SpriteRenderer renderer;

    [SerializeField]
    protected Sprite ItemImage { get; private set; }


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
