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

    //게임매니저에서 어떤버튼인지 버튼을 받아올 것
    public bool IsPushJumpBtn;
    public bool IsPushMoveBtn;

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
