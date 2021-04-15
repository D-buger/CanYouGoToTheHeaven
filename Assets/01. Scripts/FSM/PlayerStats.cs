using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [SerializeField, Range(0f, 100f)]
    private float maxSpeed = 10f;
    public float GetMaxSpeed() { return maxSpeed; }

    [SerializeField, Range(0f, 100f)]
    private float maxAcceleration = 10f, maxAirAcceleration = 1f;
    public float GetAcceleration() { return onGround ? maxAcceleration : maxAirAcceleration;  }

    public Vector3 velocity = Vector3.zero, desiredVelocity = Vector3.zero;

    [SerializeField, Range(0f, 10f)]
    private float jumpHeight = 1f;
    public float GetJumpHeight() { return jumpHeight; }

    [SerializeField]
    private Rect allowedArea = new Rect(-2.5f, -2.5f, 5f, 9f);
    public Rect GetAllowedArea() { return allowedArea; }
    [SerializeField]
    private bool activateAllowedArea = false;
    public bool GetActiveAllowedArea() { return activateAllowedArea; }

    private int maxAirJumps =10;
    public int MaxAirJumps
    {
        get {
            if (maxAirJumps != null)
                return maxAirJumps;
            else
                return 0;
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

    public bool onGround;

    public void EvauateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
