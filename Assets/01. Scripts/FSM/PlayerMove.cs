using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : FsmState<Player>
{
    PlayerStats stats;
    Rigidbody2D body;
    Vector3 velocity;

    public override void Enter(Player target)
    {
        stats = target.stats;
        body = stats.body;
        velocity = stats.velocity;
    }

    public override void Exit(Player target)
    {

    }
    
    public override void FixedUpdate(Player target)
    {
        velocity = body.velocity;
        float acceleration = stats.GetAcceleration();
        float maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(stats.velocity.x, stats.desiredVelocity.x, maxSpeedChange);
        if (stats.GetActiveAllowedArea())
        {
            CheckInAllowedArea(stats.GetAllowedArea());
        }
        stats.CurrentYPos = target.transform.position.y;
    }

    private void CheckInAllowedArea(Rect area)
    {
        if (body.position.x < area.xMin)
        {
            body.position = new Vector2(area.xMin, body.position.y);
            velocity.x = 0f;
        }
        if (body.position.x > area.xMax)
        {
            body.position = new Vector2(area.xMax, body.position.y);
            velocity.x = 0f;
        }
        if (body.position.y < area.yMin)
        {
            body.position = new Vector2(body.position.x, area.yMin);
            velocity.y = 0f;
        }
        if (body.position.y > area.yMax)
        {
            body.position = new Vector2(body.position.x, area.yMax);
            velocity.y = 0f;
        }
    }

    public override void HandleInput(Player target)
    {
        if (!target.IsPushMoveBtn)
        {
            target.ChangeState(ePlayerState.Idle);
        }
        else if (target.IsPushJumpBtn)
        {
            target.ChangeState(ePlayerState.Jump);
        }
    }

    public override void Update(Player target)
    {
        //플레이어 이동 애니메이션 재생
    }
}
