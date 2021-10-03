using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMonsterProjectile : MonoBehaviour
{
    int damage = 2;
    float velocity = 1f;
    float homingDelay = 0.2f;
    float homingDelayCounter;
    float lifetime = 6f;

    Vector2 destination;

    Rigidbody2D rb2d;

    public void StartingInitialize(int _damage, float _velocity, float _lifetime, float _homingDelay = 0.1f)
    {
        damage = _damage; velocity = _velocity; lifetime = _lifetime; homingDelay = _homingDelay; homingDelayCounter = homingDelay;
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            _collision.gameObject.GetComponent<Player>().stats.CurrentHp -= damage;
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (homingDelayCounter >= 0)
        {
            homingDelayCounter -= deltaTime;
        }
        else
        {
            homingDelayCounter = homingDelay;
            LookToDestination(MonsterManager.instance.player.transform.position);
            rb2d.velocity = transform.right * velocity;
        }
        lifetime -= deltaTime;

        if (lifetime <= 0)
        {
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
    }

    void LookToDestination(Vector3 _destination)
    {
        Vector2 dir = _destination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
}
