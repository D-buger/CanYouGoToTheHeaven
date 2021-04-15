using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : FsmState<Player>
{
    PlayerStats stats;
    Vector2 playerInput;

    public override void Enter(Player target)
    {
        Debug.Log("PlayerState : Move");
        stats = target.stats;
        
        playerInput = target.input.GetKey(target.input.MoveLeftKey) ? Vector2.left : Vector2.right;
        target.stats.desiredVelocity =  playerInput * target.stats.GetMaxSpeed();
    }

    public override void Exit(Player target)
    {

    }
    
    public override void FixedUpdate(Player target)
    {
        stats.velocity.x = stats.body.velocity.x;
        float acceleration = stats.GetAcceleration();
        float maxSpeedChange = acceleration * Time.deltaTime;
        stats.velocity.x =
            Mathf.MoveTowards(stats.velocity.x, stats.desiredVelocity.x, maxSpeedChange);
        if (stats.GetActiveAllowedArea())
        {
            CheckInAllowedArea(stats.GetAllowedArea());
        }
        stats.CurrentYPos = target.transform.position.y;
        stats.body.velocity = new Vector2( target.stats.velocity.x, stats.body.velocity.y );
    }

    private void CheckInAllowedArea(Rect area)
    {
        if (stats.body.position.x < area.xMin)
        {
            stats.body.position = new Vector2(area.xMin, stats.body.position.y);
            stats.velocity.x = 0f;
        }
        if (stats.body.position.x > area.xMax)
        {
            stats.body.position = new Vector2(area.xMax, stats.body.position.y);
            stats.velocity.x = 0f;
        }
        if (stats.body.position.y < area.yMin)
        {   
            stats.body.position = new Vector2(stats.body.position.x, area.yMin);
            stats.velocity.y = 0f;
        }   
        if (stats.body.position.y > area.yMax)
        {   
            stats.body.position = new Vector2(stats.body.position.x, area.yMax);
            stats.velocity.y = 0f;
        }
    }

    public override void HandleInput(Player target)
    {
        if (playerInput == Vector2.left? !target.input.GetKey(target.input.MoveLeftKey)
            : !target.input.GetKey(target.input.MoveRightKey)) //MoveKey
        {
            target.ChangeState(ePlayerState.Idle);
        }
        else if (target.input.GetKey(target.input.JumpKey))
        {
            target.ChangeState(ePlayerState.Jump);
        }
    }

    public override void Update(Player target)
    {
        //플레이어 이동 애니메이션 재생
    }
}
