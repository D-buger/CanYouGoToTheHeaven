using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : WalkMonster
{
    [SerializeField, Tooltip("������ �þ� ����.\n�ش� ���� ���� �÷��̾ ������ �÷��̾ �߰��Ѵ�.")] protected float visualRange;
    [SerializeField, Tooltip("���Ͱ� �߰��� ������ �� ������ �ð�\n(�÷��̾�� �Ÿ��� VisualRange�� �Ѿ ����)")] float chasingTime;
    protected bool detectPlayer;

    protected WaitForSeconds waitForChasingTime;
    protected Coroutine chasingCoroutine = null;

    protected override void SettingVariables()
    {
        base.SettingVariables();
        StopAllCoroutines();
        detectPlayer = false;
        visualRange = StringToFloat(GetDataWithVariableName("CognitiveRange"));
        chasingTime = StringToFloat(GetDataWithVariableName("ChasingDuration"));
    }

    protected override void OperateAwake()
    {
        base.OperateAwake();
    }

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
        if (player == null)
        {
            player = MonsterManager.instance.player;
        }
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
