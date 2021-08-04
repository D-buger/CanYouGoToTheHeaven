using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics
{
    public PlayerPhysics(PlayerStats stat) => this.stat = stat;
    PlayerStats stat;

    public void PhysicsUpdate()
    {
        stat.velocity = stat.body.velocity;
        float acceleration = stat.GetAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        stat.velocity.x =
            Mathf.MoveTowards(stat.velocity.x, stat.desiredVelocity.x, maxSpeedChange);
        stat.trans.rotation = Quaternion.Euler(0, 0, -Mathf.MoveTowardsAngle(0, stat.RotateAmount ,stat.velocity.x * 3));

        stat.body.velocity = stat.velocity;
        stat.onGround = false;
    }

    public bool IsOnGround() => stat.onGround;

    public void Moving(float input)
    {
        stat.desiredVelocity.x = input * stat.GetMaxSpeed;
    }
    
}
