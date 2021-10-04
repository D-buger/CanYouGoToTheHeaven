using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public int value = 1;
    float startingBounceTime = 1.2f;
    float currentTime;
    float velocity = 3f;
    Vector2 bounceDestination;

    CircleCollider2D circleColl;

    private void OnEnable()
    {
        startingBounceTime = 1.4f;
        currentTime = 0f;
        velocity = 8f;
        bounceDestination = new Vector2(transform.position.x + Random.Range(1f, 4f), transform.position.y + Random.Range(1f, 4f));
        LookToDestination(bounceDestination);

        circleColl.enabled = false;
    }

    private void Awake()
    {
        circleColl = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float deltaTime = Time.deltaTime;
        currentTime += deltaTime;

        if (currentTime <= startingBounceTime)
        {
            transform.Translate(transform.right * velocity * deltaTime);
            startingBounceTime -= deltaTime;
        }
        else if (currentTime <= 6f)
        {
            if (circleColl.enabled == false)
            {
                circleColl.enabled = true;
            }
            velocity = velocity < 17f ? velocity + 5f * deltaTime : 17f;
            LookToDestination(MonsterManager.instance.player.transform.position);
            transform.Translate(transform.right * velocity * deltaTime);
        }
        else if (currentTime <= 10f)
        {
            LookToDestination(MonsterManager.instance.player.transform.position);
            transform.Translate((MonsterManager.instance.player.transform.position - transform.position).normalized * velocity * 2f * deltaTime);
        }
        else
        {
            AddPlayerSoulAmount();
        }
    }

    void LookToDestination(Vector3 _destination)
    {
        Vector2 dir = _destination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PlayerTag))
        {
            AddPlayerSoulAmount();
        }
    }

    void AddPlayerSoulAmount()
    {
        StageManager.Instance.Stat.Soul += value;
        MonsterPoolManager.instance.ReturnObject(gameObject);
    }
}
