using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : WalkMonster
{
    void Start()
    {
        OperateStart();
    }

    void Update()
    {
        CheckFront();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        OperateOnCollisionEnter2D(_collision);
    }
}
