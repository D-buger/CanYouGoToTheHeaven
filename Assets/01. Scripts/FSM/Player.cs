using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
}

public class Player : MonoBehaviour
{
    public StateMachine<Player> StateMachine { get; private set; }
    public PlayerStats stats;

    private Dictionary<ePlayerState, FsmState<Player>> stateByEnum;

    public InputManager input => GameManager.Instance.input;

    private void Awake()
    {
        stats = new PlayerStats()
        {
            body = transform.GetComponent<Rigidbody2D>(),
            previousYPos = transform.position.y
        };

        stateByEnum = new Dictionary<ePlayerState, FsmState<Player>>();
        stateByEnum.Add(ePlayerState.Idle , new PlayerIdle());
        stateByEnum.Add(ePlayerState.Move , new PlayerMove());
        stateByEnum.Add(ePlayerState.Jump , new PlayerJump());
        stateByEnum.Add(ePlayerState.Attack , new PlayerAttack());


        StateMachine = new StateMachine<Player>();
        StateMachine.Init(this, new PlayerIdle());
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
        Debug.Log("current velocity : " + stats.velocity);
    }

    public void ChangeState(ePlayerState state)
    {
        StateMachine.ChangeState(stateByEnum[state]);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        stats.EvauateCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        stats.EvauateCollision(collision);
    }
}
