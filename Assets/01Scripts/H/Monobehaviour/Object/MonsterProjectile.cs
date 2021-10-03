using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] byte damage;
    [SerializeField] float velocity;
    int reflectCount = 3;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 incidenceVec;
    [SerializeField] Vector3 reflectionVec;

    float lifetimeCounter;

    int recochetCount = 5;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.right * velocity;
    }

    private void Update()
    {
        if (lifetimeCounter >= float.Epsilon)
        {
            lifetimeCounter -= Time.deltaTime;
        }
        else
        {
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
    }

    public void Initialize(int _damage, float _velocity, float _lifetime, float _angle)
    {
        damage = (byte)_damage;
        lifetimeCounter = _lifetime;
        velocity = _velocity;
        transform.rotation = Quaternion.Euler(0, 0, _angle);
        reflectCount = 3;
    }

    void LookToDestination(Vector3 _destination)
    {
        Vector2 dir = _destination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            _collision.gameObject.GetComponent<Player>().stats.CurrentHp -= damage;
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
        else if (reflectCount <= 0)
        {
            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Platform") || _collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            reflectCount -= 1;
            Vector3 hitPos = _collision.contacts[0].point;
            incidenceVec = hitPos - startPos;
            reflectionVec = Vector3.Reflect(incidenceVec, _collision.contacts[0].normal);
            LookToDestination(transform.position + reflectionVec);
            startPos = transform.position;
        }
    }
}
