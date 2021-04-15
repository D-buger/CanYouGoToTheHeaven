using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : FsmState<Player>
{
    private float delay = 0f;
    private float reloading = 0f;

    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Attack");
    }

    public override void Exit(Player target)
    {

    }

    public override void FixedUpdate(Player target)
    {

    }

    public override void HandleInput(Player target)
    {

    }

    public override void Update(Player target)
    {

    }
}
