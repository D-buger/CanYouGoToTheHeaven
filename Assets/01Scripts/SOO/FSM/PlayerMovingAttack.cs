using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingAttack : FsmState<Player>
{
    public override void Enter(Player target)
    {
        target.anim.SetBool("isAttack", true);
    }

    public override void Exit(Player target)
    {
        if (!target.input.BehaviourActive)
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
        target.physics.Moving(target.input.Joystick.horizontalValue);
    }

    public override void HandleInput(Player target)
    {
        if (target.input.AttackActive)
        {
            if (!target.input.IsMove)
            {
                target.ChangeState(ePlayerState.Attack);
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

        if (target.stats.CurrentHp <= 0)
            target.ChangeState(ePlayerState.Dead);
    }
    
}
