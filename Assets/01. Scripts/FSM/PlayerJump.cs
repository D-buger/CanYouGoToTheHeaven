using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : FsmState<Player>
{
    private int jumpPhase = 0;

    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Jump");
        
    }

    public override void Exit(Player target)
    {
        //target.anim.SetBool("Jump", false);
    }

    public override void Once(Player target)
    {
        target.physics.Jump(jumpPhase);
    }

    public override void Update(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {

    }

    public override void HandleInput(Player target)
    {
        if (jumpPhase > target.stats.MaxAirJumps)
        {
            target.ChangeState(ePlayerState.Attack);
        }
        if (target.input.IsMove()) //moveKey
        {
            target.ChangeState(ePlayerState.Move);
        }

        target.ChangeState(ePlayerState.Idle);
    }
}
