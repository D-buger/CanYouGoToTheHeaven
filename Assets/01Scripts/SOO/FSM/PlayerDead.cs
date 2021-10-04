using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : FsmState<Player>
{
    public static event System.Action deadCallback;

    public override void Enter(Player target)
    {
        deadCallback?.Invoke();
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

    public override void Once(Player target)
    {

    }

    public override void Update(Player target)
    {

    }
}
