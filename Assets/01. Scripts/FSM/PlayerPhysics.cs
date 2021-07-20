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
        float acceleration = stat.GetAcceleration();
        float maxSpeedChange = acceleration * Time.deltaTime;
        stat.velocity.x =
            Mathf.MoveTowards(stat.velocity.x, stat.desiredVelocity.x, maxSpeedChange);

        stat.CurrentYPos = stat.trans.position.y;

        stat.body.velocity = stat.velocity;
        stat.onGround = false;
    }
    
    int Shot(int shotPhase)
    {
        shotPhase++;
        float rebound = Mathf.Sqrt(-1f * Physics2D.gravity.y / shotPhase);
        stat.velocity.y = rebound;
        return shotPhase;

    }

    public bool IsOnGround() => stat.onGround;

    public void Moving(Vector2 input)
    {
        input = Vector2.ClampMagnitude(input, 1f);

        stat.desiredVelocity = input * stat.GetMaxSpeed();
    }

    public int Jump(int jumpPhase)
    {
        if (stat.onGround || jumpPhase < stat.MaxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * stat.GetJumpHeight());
            if (stat.velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - stat.velocity.y, 0f);
            }
            stat.velocity.y += jumpSpeed;
            stat.body.velocity = stat.velocity;
        }
        return jumpPhase;
    }
}
