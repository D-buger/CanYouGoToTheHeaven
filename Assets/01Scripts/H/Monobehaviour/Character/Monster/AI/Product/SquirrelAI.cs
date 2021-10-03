using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : WalkMonster
{
    private void OnEnable()
    {
        OperateOnEnable();
    }

    void Start()
    {
        OperateStart();
    }

    void Update()
    {
        CheckDistanceFromPlayer();
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
