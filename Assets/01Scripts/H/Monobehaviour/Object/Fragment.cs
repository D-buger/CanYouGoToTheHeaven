using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    int damage;
    float velocity;

    Rigidbody2D rb2d;

    public void Initialize(int _damage, float _velocity)
    {
        damage = _damage; velocity = _velocity;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(velocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= damage;
            Destroy(gameObject);
        }
        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Tile"))
        {
            Destroy(gameObject);
        }
    }
}
