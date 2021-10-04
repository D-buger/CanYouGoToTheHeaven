using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    SpriteRenderer sprite;

    const float duration = 3f;
    float durationCounter;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        durationCounter = duration;
    }

    private void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            durationCounter -= Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, durationCounter / duration);
            if (durationCounter <= float.Epsilon)
            {
                Destroy(gameObject);
            }
        }
    }
}
