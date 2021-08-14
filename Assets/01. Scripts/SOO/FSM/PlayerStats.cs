using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [SerializeField, Range(0f, 10f)]
    private float maxSpeed = 5f;
    public float GetMaxSpeed => maxSpeed;

    [SerializeField, Range(0f, 2f)]
    private float moveSpeed = 1f;
    public float GetMoveSpeed => moveSpeed;

    [SerializeField, Range(0f, 10f)]
    private float maxAcceleration = 20f, maxAirAcceleration = 15f;
    public float GetAcceleration => onGround ? maxAcceleration : maxAirAcceleration; 

    public Vector3 velocity = Vector3.zero, desiredVelocity = Vector3.zero;
    public float aVelocity = 0, aDesiredVelocity = 0;

    [SerializeField, Range(0f, 180f)]
    private float rotateAmount = 45f;
    public float RotateAmount => rotateAmount;

    [SerializeField, Range(0f, 10f)]
    private float jumpHeight = 1f;
    public float GetJumpHeight => jumpHeight;
    
    private int maxAirJumps = 1;
    public int MaxAirJumps
    {
        get {
                return maxAirJumps;
        }
        set
        {
            if (value + maxAirJumps < 0) maxAirJumps = 0;
            else { maxAirJumps += value; }
        }
    }
    
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
    }
}
