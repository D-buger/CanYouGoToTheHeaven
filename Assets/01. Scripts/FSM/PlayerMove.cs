using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : FsmState<Player>
{
    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Move");

        if(target.stats.onGround)
            target.anim.SetBool("Run", true);
    }

    public override void Exit(Player target)
    {
        target.anim.SetBool("Run", false);
    }

    public override void Once(Player target)
    {

    }

    public override void Update(Player target)
    {
        //TODO : 플레이어 이미지 뒤집기
    }

    public override void FixedUpdate(Player target)
    {
        target.physics.Moving(target.input.MoveKeyInput());
    }
    
    public override void HandleInput(Player target)
    {
        if (target.input.GetKey(target.input.JumpKey))
        {
            target.ChangeState(ePlayerState.Jump);
        }
    }
}
