using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : WalkMonster
{
    [SerializeField, Tooltip("몬스터의 시야 범위.\n해당 범위 내에 플레이어가 들어오면 플레이어를 발견한다.")] protected float visualRange;
    [SerializeField, Tooltip("몬스터가 추격을 포기할 때 까지의 시간\n(플레이어와 거리가 VisualRange를 넘어간 이후)")] float chasingTime;
    protected bool detectPlayer;

    protected WaitForSeconds waitForChasingTime;
    protected WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    protected Coroutine chasingCoroutine = null;


    protected override void OperateStart()
    {
        base.OperateStart();
        waitForChasingTime = new WaitForSeconds(chasingTime);
    }

    protected override void OperateUpdate()
    {
        SearchPlayer();
        if (detectPlayer)
        {
            return;
        }
        base.OperateUpdate();
    }

    protected override void OperateFixedUpdate()
    {
        if (detectPlayer)
        {
            return;
        }
        base.OperateFixedUpdate();
    }

    IEnumerator StopChasing()
    {
        yield return waitForChasingTime;
        detectPlayer = false;
    }

    protected void SearchPlayer()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= visualRange)
        {
            if (chasingCoroutine != null)
            {
                StopCoroutine(chasingCoroutine);
                chasingCoroutine = null;
            }
            detectPlayer = true;
        }
        else
        {
            if (chasingCoroutine == null)
            {
                chasingCoroutine = StartCoroutine(StopChasing());
            }
        }
    }
}
