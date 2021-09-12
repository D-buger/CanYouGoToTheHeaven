using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_Monster : HTest_Character
{
    public enum MonsterGrade
    {
        D, C, B, A, S, ELITE
    }

    public MonsterGrade monsterGrade;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float detectDistance;

    protected Rigidbody2D rb2d;

    protected HTest_IMovementBehavior currentMovement;
    protected GameObject playersss;

    private void Awake()
    {
        playersss = GameObject.Find("Playersss");
        currentMovement = new HTest_IMovementBehavior_Static();
    }

    protected void SetMovementBehavior(HTest_IMovementBehavior behavior)
    {
        if (currentMovement.GetType() == behavior.GetType())
        {
            Debug.LogError($"{gameObject.name}오브젝트는 이미 {behavior}상태를 실행중입니다");
            return;
        }
        currentMovement.OperateExit(rb2d, playersss, movementSpeed);
        currentMovement = behavior;
        currentMovement.OperateEnter(rb2d, playersss, movementSpeed);
    }

    protected bool DetectPlayer()
    {
        if (Vector2.Distance(playersss.transform.position, transform.position) <= detectDistance)
        {
            return true;
        }
        return false;
    }

    protected override void InflictDamage(float _damage)
    {
        throw new System.NotImplementedException();
    }
}
