using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : FsmState<Player>
{
    int jumpPhase = 0;

    public override void Enter(Player target)
    {

    }

    public override void Exit(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {
        if(target.stats.onGround || jumpPhase < target.stats.MaxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * target.stats.GetJumpHeight());
            if(target.stats.velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - target.stats.velocity.y, 0f);
            }
            target.stats.velocity.y += jumpSpeed;
        }
        target.stats.onGround = false;
    }

    public override void HandleInput(Player target)
    {
        
    }

    public override void Update(Player target)
    {

    }

    
}
