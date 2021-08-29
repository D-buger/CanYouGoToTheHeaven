using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] byte damage;
    [SerializeField] Vector2 velocity;

    int recochetCount = 30;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int _damage, Vector2 _velocity)
    {
        damage = (byte)_damage;
        velocity = _velocity;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = velocity;
    }

    

    Vector2 lastVelo;
    private void Update()
    {
        lastVelo = rb2d.velocity;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (_collision.gameObject.layer == LayerMask.NameToLayer("Tile"))
        {
            Vector2 dir = Vector2.Reflect(lastVelo.normalized, _collision.contacts[0].normal);
            rb2d.velocity = dir * velocity;
        }
    }
}
