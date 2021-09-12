using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_Player : HTest_Character
{
    SpriteRenderer sprite;
    [SerializeField] Sprite spri;

    protected override void InflictDamage(float _damage)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        sprite.sprite = spri;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HTest_Monster>().KillCharacter();
            hitPoint -= 1;
        }
    }
}
