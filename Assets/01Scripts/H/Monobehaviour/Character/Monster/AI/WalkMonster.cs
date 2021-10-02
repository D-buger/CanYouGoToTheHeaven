using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMonster : HMonster
{
    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;

    [SerializeField] protected float movementSpeed;
    [SerializeField, Range(-1f, 1f)] float checkRayAddLength;
    protected bool isRight = false;
    protected int moveDir = 0;

    protected override void SettingVariables()
    {
        base.SettingVariables();
        movementSpeed = StringToInteger(GetDataWithVariableName("MovementSpeed"));
    }

    protected override void OperateStart()
    {
        base.OperateStart();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void OperateUpdate()
    {

    }

    protected virtual void OperateFixedUpdate()
    {

    }

    protected void MoveForward()
    {
        rb2d.MovePosition(transform.position + Vector3.right * moveDir * movementSpeed * Time.deltaTime);
    }

    protected void CheckFront()
    {
        CheckFrontGround();
        CheckFrontWall();
    }

    protected void CheckFrontGround()
    {
        Vector2 front = new Vector2(transform.position.x + (moveDir * transform.localScale.x) + (0.1f * moveDir) + (checkRayAddLength * -moveDir), transform.position.y);
        RaycastHit2D platformCheckRay = Physics2D.Raycast(front, Vector2.up, 1, LayerMask.GetMask("Platform"));
        Debug.DrawRay(front, Vector2.up, Color.cyan);
        if (platformCheckRay.collider == null)
        {
            isRight = !isRight;
        }
        moveDir = isRight ? 1 : -1;
        sprite.flipX = !isRight;
    }

    protected void CheckFrontWall()
    {
        RaycastHit2D wallCheckRay = Physics2D.Raycast(new Vector2(transform.position.x + (checkRayAddLength * -moveDir), transform.position.y), Vector2.right * moveDir, (1 * transform.localScale.x), LayerMask.GetMask("Wall"));
        Debug.DrawRay(new Vector2(transform.position.x + (checkRayAddLength * -moveDir), transform.position.y), Vector2.right * moveDir * transform.localScale.x, Color.cyan);
        if (wallCheckRay)
        {
            isRight = !isRight;
        }
        moveDir = isRight ? 1 : -1;
        sprite.flipX = !isRight;
    }
}
