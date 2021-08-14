using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics
{
    public PlayerPhysics(PlayerStats stat) => this.stat = stat;
    PlayerStats stat;
    float input;

    public void PhysicsUpdate()
    {
        stat.velocity = stat.body.velocity;
        float acceleration = stat.GetAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        stat.velocity.x =
            Mathf.MoveTowards(stat.velocity.x, stat.desiredVelocity.x, maxSpeedChange);
        stat.aVelocity = 
            -Mathf.MoveTowardsAngle(stat.aVelocity, stat.aDesiredVelocity, maxSpeedChange * 10);
        Debug.Log(stat.aVelocity);

        stat.trans.rotation = Quaternion.Euler(0, 0, stat.aVelocity);

        stat.body.velocity = stat.velocity;
        stat.onGround = false;
    }

    public bool IsOnGround() => stat.onGround;

    public void Turning(float _input)
    {
        input = _input;
        stat.aDesiredVelocity = input * stat.RotateAmount;
        
    }

    public void Moving(float _input)
    {
        input = _input;
        stat.desiredVelocity.x = input * stat.GetMaxSpeed;
    }
    
}
