using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : FsmState<Player>
{


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
    }
}
