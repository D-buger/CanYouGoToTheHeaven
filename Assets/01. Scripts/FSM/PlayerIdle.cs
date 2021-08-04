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
        if (target.input.joystick.horizontalValue != 0)
        {
            target.ChangeState(ePlayerState.Move);
        }
    }
}
