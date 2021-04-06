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

    }

    public override void HandleInput(Player target)
    {
        if (GameManager.Instance.input.GetKey(GameManager.Instance.input.AttackKey))
        {
            target.ChangeState(ePlayerState.Jump);
        }
        else if (target.IsPushMoveBtn)
        {
            target.ChangeState(ePlayerState.Move);
        }
    }

    public override void Update(Player target)
    {

    }
}
