using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : FsmState<Player>
{
    private int jumpPhase = 0;
    private bool desiredJump = false;

    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Jump");
        if (target.stats.onGround)
            jumpPhase = 0;
        desiredJump = true;
    }

    public override void Exit(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {
        target.stats.velocity.y = target.stats.body.velocity.y;
        if (desiredJump)
        {
            desiredJump = false;
            Jump(target);
        }


        target.stats.CurrentYPos = target.transform.position.y;
        target.stats.body.velocity = target.stats.velocity;
    }

    void Jump(Player target)
    {
        //Debug.Log(target.stats.velocity.y);
        if (target.stats.onGround || jumpPhase < target.stats.MaxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * target.stats.GetJumpHeight());
            if (target.stats.velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - target.stats.velocity.y, 0f);
            }
            target.stats.velocity.y += jumpSpeed;
            //Debug.Log(target.stats.velocity.y);
        }
    }

    public override void HandleInput(Player target)
    {
        if (!desiredJump)
        {
            if (target.input.GetKey(target.input.MoveLeftKey) || target.input.GetKey(target.input.MoveRightKey)) //moveKey
            {
                target.ChangeState(ePlayerState.Move);
            }

            target.ChangeState(ePlayerState.Idle);
        }
    }

    public override void Update(Player target)
    {

    }

    
}
