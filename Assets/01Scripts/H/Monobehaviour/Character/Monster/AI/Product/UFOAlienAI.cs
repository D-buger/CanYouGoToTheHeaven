using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAlienAI : WalkMonster
{
    bool isAlreadyDetectPlayer;
    [SerializeField, Range(1f, 20f)] float height;
    [SerializeField] float visualRange;
    bool isAttacking = false;

    void Start()
    {
        OperateStart();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (!isAlreadyDetectPlayer)
        {
            FindPlayer();
            return;
        }
        CheckFrontWall();
    }

    void FindPlayer()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= visualRange)
        {
            isAlreadyDetectPlayer = true;
        }
    }

    private void FixedUpdate()
    {
        if (isAlreadyDetectPlayer)
        {
            if (!isAttacking)
            {
                if (transform.position.y - player.transform.position.y >= height) //너무 높게 올라가면
                {
                    rb2d.MovePosition(rb2d.position + Vector2.right * moveDir * movementSpeed * Time.deltaTime);
                }
                else
                {
                    rb2d.MovePosition(new Vector2(rb2d.position.x + (moveDir * movementSpeed * Time.deltaTime), rb2d.position.y + 4 * Time.deltaTime));
                }
            }
        }
    }

    [System.Serializable]
    struct DebugOption
    {
        public bool showVisualRange;
    }
    [SerializeField] DebugOption debugOption;

    private void OnDrawGizmos()
    {
        if (debugOption.showVisualRange)
        {
            Gizmos.DrawWireSphere(transform.position, visualRange);
            Gizmos.color = Color.cyan;
        }
    }
}
