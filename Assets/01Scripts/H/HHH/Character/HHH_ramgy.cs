using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHH_ramgy : HHH_Enemy
{
    HHH_MoveStrate currentMoveState;
    HHH_MoveStrate_TrackingTarget trackingTarget = new HHH_MoveStrate_TrackingTarget();
    HHH_MoveStrate_Static staticObj = new HHH_MoveStrate_Static();

    Rigidbody2D rb2d;
    [SerializeField] float speed;
    bool detectPlayer;
    [SerializeField] float detectDistance;
    [SerializeField] float giveUp;

    Coroutine moveCoroutine = null;

    private void Start()
    {
        currentMoveState = staticObj;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<HHH_Player>();
    }

    private void Update()
    {

    }

    IEnumerator giveup()
    {
        yield return new WaitForSeconds(giveUp); //�����ϸ� ĳ�̽�ų��
        Debug.Log("��������");
        currentMoveState = staticObj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾��");
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            currentMoveState = trackingTarget;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾��");
            moveCoroutine = StartCoroutine("giveup");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    private void FixedUpdate()
    {
        currentMoveState.OperateUpdate(rb2d, player.gameObject, speed);
    }
}
