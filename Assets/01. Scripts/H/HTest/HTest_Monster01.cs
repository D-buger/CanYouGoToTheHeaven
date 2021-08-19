using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_Monster01 : HTest_Monster
{
    private void Update()
    {
        if (DetectPlayer()) //일정 범위에 있는 동안이나 발견 후 일정 시간동안 지속적으로 따라오게 만들것
        {
            SetMovementBehavior(new HTest_IMovementBehavior_TrackingTarget());
        }
        else
        {
            SetMovementBehavior(new HTest_IMovementBehavior_Static());
        }
        currentMovement.OperateUpdate(rb2d, playersss, movementSpeed);
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    bool DetectPlayer()
    {
        if (Vector2.Distance(playersss.transform.position, transform.position) <= detectDistance)
        {
            return true;
        }
        return false;
    }
}
