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

    public override void Once(Player target)
    {

    }

    public override void Update(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {

    }

    public override void HandleInput(Player target)
    {
        if (target.input.GetKey(target.input.JumpKey))
        {
            target.ChangeState(ePlayerState.Jump);
        }
        else if (target.input.IsMove()) //MoveKey
        {
            target.ChangeState(ePlayerState.Move);
        }
        else if (target.input.GetKey(target.input.AttackKey))
        {
            target.ChangeState(ePlayerState.Attack);
        }
    }
}
