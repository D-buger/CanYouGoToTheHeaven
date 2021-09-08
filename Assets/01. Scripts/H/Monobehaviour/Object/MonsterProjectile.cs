using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] byte damage;
    [SerializeField] float velocity;
    [SerializeField] Vector3 destination;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 incidenceVec;
    [SerializeField] Vector3 reflectionVec;
    bool rerere;

    int recochetCount = 5;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!rerere)
        {
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }
    }

    public void Initialize(int _damage, float _velocity, Vector3 _destination)
    {
        damage = (byte)_damage;
        velocity = _velocity;
        destination = _destination;
        LookToDestination(destination);
    }

    void LookToDestination(Vector3 _destination)
    {
        Vector2 dir = _destination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.layer == LayerMask.NameToLayer("Tile"))
        {
            Vector3 hitPos = _collision.contacts[0].point;
            incidenceVec = hitPos - startPos;
            reflectionVec = Vector3.Reflect(incidenceVec, _collision.contacts[0].normal);
            rerere = true;
            rb2d.velocity = reflectionVec.normalized * velocity;
            LookToDestination(reflectionVec);
            startPos = transform.position;
        }
    }
}
