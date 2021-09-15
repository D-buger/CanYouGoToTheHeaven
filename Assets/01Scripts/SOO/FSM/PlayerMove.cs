using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : FsmState<Player>
{
    public override void Enter(Player target)
    {
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
        target.physics.Moving(target.input.joystick.horizontalValue);
    }
    
    public override void HandleInput(Player target)
    {
        if (target.input.behaviourActive)
        {
            if (!target.input.IsMove)
            {
                target.ChangeState(ePlayerState.Attack);
            }
            else
            {
                target.ChangeState(ePlayerState.MovingAttack);
            }
        }
        else
        {
            if (!target.input.IsMove)
            {
                target.ChangeState(ePlayerState.Idle);
            }
        }
    }
}
