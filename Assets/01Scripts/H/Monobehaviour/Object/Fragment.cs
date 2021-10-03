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
        if (rb2d == null)
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(velocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag(TagManager.PlayerTag))
        {
            StageManager.Instance.Stat.CurrentHp -= damage;
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
    }
}
