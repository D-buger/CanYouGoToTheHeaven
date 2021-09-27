using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics
{
    public PlayerPhysics(PhysicsStats _stat) => stat = _stat;

    private PhysicsStats stat;
    private float input;

    public void PhysicsUpdate()
    {
        stat.velocity = stat.body.velocity;
        float acceleration = stat.GetAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        stat.velocity.x =
            Mathf.MoveTowards(stat.velocity.x, stat.desiredVelocity.x, maxSpeedChange);
        stat.aVelocity = 
            -(int)Mathf.MoveTowardsAngle(0, stat.rotateAmount,
            (stat.velocity.x / stat.maxSpeed) * stat.rotateAmount);
        
        if(!stat.limitLeft || !stat.limitRight)
            stat.trans.rotation = Quaternion.Euler(0, 0, stat.aVelocity);

        stat.body.velocity = stat.velocity;
        stat.onGround = false;
        stat.limitLeft = false;
        stat.limitRight = false;
    }

    public bool IsOnGround() => stat.onGround;

    public void Shot()
    {
        float rebound = Mathf.Sqrt(-1f * Physics2D.gravity.y );
        stat.velocity.y = rebound;
        stat.body.velocity = stat.velocity;
    }

    public void Moving(float _input)
    {
        input = _input;
        stat.desiredVelocity.x = input * stat.maxSpeed;
        if (stat.limitLeft)
        {
            stat.desiredVelocity.x = Mathf.Max(0 , stat.desiredVelocity.x);
        }
        else if (stat.limitRight)
        {
            stat.desiredVelocity.x = Mathf.Min(0, stat.desiredVelocity.x);
        }
    }
    
}
