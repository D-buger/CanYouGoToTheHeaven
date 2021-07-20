using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : FsmState<Player>
{
    private float delay = 0f;
    private float reloading = 0f;

    private int maxShotPhase = 10;
    private int shotPhase = 0;

    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Attack");
        
    }

    public override void Exit(Player target)
    {
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
    }

    public override void HandleInput(Player target)
    {
        if (target.input.IsMove()) //moveKey
        {
            target.ChangeState(ePlayerState.Move);
        }

        target.ChangeState(ePlayerState.Idle);
    }
}
