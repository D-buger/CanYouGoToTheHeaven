using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : FsmState<Player>
{
    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Attack");
        target.anim.SetBool("isAttack", true);
    }

    public override void Exit(Player target)
    {
        if (!target.input.behaviourActive)
            target.anim.SetBool("isAttack", false);
    }

    public override void Once(Player target)
    {

    }

    public override void Update(Player target)
    {
        target.physics.Shot();
    }

    public override void FixedUpdate(Player target)
    {

    }

    public override void HandleInput(Player target)
    {
        if (target.input.behaviourActive)
        {
            if (target.input.IsMove)
            {
                target.ChangeState(ePlayerState.MovingAttack);
            }
        }
        else
        {
            if (target.input.IsMove)
            {
                target.ChangeState(ePlayerState.Move);
            }
            else
            {
                target.ChangeState(ePlayerState.Idle);
            }
        }
    }
}
