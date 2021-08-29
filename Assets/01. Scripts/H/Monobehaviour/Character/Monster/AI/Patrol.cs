using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : HMonster
{
    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;

    [SerializeField] protected float movementSpeed;
    protected bool isRight = true;
    protected int moveDir = 0;

    protected override void OperateStart()
    {
        base.OperateStart();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void OperateUpdate()
    {
        CheckFront();
        moveDir = isRight ? 1 : -1;
        sprite.flipX = !isRight;
    }

    protected virtual void OperateFixedUpdate()
    {
        rb2d.MovePosition(transform.position + Vector3.right * moveDir * movementSpeed * Time.deltaTime);
    }

    protected void CheckFront()
    {
        Vector2 front = new Vector2(transform.position.x + (moveDir * transform.localScale.x), transform.position.y);
        RaycastHit2D platformCheckRay = Physics2D.Raycast(front, Vector2.up, 1, LayerMask.GetMask("Tile"));
        RaycastHit2D wallCheckRay = Physics2D.Raycast(transform.position, Vector2.right * moveDir, (1 * transform.localScale.x), LayerMask.GetMask("Tile"));
        Debug.DrawRay(front, Vector2.up, Color.cyan);
        Debug.DrawRay(transform.position, Vector2.right * moveDir * transform.localScale.x, Color.cyan);
        if (platformCheckRay.collider == null || wallCheckRay)
        {
            isRight = !isRight;
        }
    }
}
