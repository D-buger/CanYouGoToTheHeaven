using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int MaxHP = 3;
    private int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = Mathf.Min(value, MaxHP);
            currentHP = Mathf.Max(0, currentHP);
        }
    }

    [SerializeField, Range(0f, 10f), ReadOnly]
    public readonly float maxSpeed = 5f;

    [SerializeField, Range(0f, 2f), ReadOnly]
    public readonly float moveSpeed = 1f;

    [SerializeField, Range(0f, 10f)]
    private float maxAcceleration = 20f, maxAirAcceleration = 15f;
    public float GetAcceleration => onGround ? maxAcceleration : maxAirAcceleration; 

    public Vector3 velocity = Vector3.zero, desiredVelocity = Vector3.zero;
    public float aVelocity = 0, aDesiredVelocity = 0;

    [SerializeField, Range(0f, 180f), ReadOnly]
    public readonly float rotateAmount = 45f;
    
    private float currentYSpeed = 0f;
    public float previousYPos { get; set; }
    public float CurrentYPos
    {
        get
        {
            return currentYSpeed;
        }
        set
        {
            currentYSpeed = Mathf.Abs(value - previousYPos);
            previousYPos = value;
        }
    }

    public Rigidbody2D body;
    public Transform trans;

    public bool onGround;

    public bool limitLeft;
    public bool limitRight;

    public void EvauateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            onGround |= normal.y <= 0.9f;
        }

        if (collision.collider.CompareTag("Wall"))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;
                limitLeft |= normal.x >= 0.9f;
            }
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;
                limitRight |= normal.x <= 0.9f;
            }
        }
    }

    public void Set(Transform _trans, Rigidbody2D rigid, float y)
    {
        trans = _trans;
        body = trans.GetComponent<Rigidbody2D>();
        previousYPos = trans.position.y;
        currentHP = MaxHP;
    }
}
