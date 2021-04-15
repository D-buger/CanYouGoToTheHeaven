using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : FsmState<Player>
{
    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Idle");
    }

    public override void Exit(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {
        target.stats.velocity = target.stats.body.velocity;
    }

    public override void HandleInput(Player target)
    {
        if (target.input.GetKey(target.input.JumpKey))
        {
            target.ChangeState(ePlayerState.Jump);
        }
        else if (target.input.GetKey(target.input.MoveLeftKey) || target.input.GetKey(target.input.MoveRightKey)) //MoveKey
        {
            target.ChangeState(ePlayerState.Move);
        }
    }

    public override void Update(Player target)
    {

    }
}
