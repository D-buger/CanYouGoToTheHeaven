using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Idle,
    Move,
    MovingAttack,
    Attack,
}

public class Player : MonoBehaviour
{
    public StateMachine<Player> StateMachine { get; private set; }
    public PlayerPhysics physics;
    public PlayerStats stats;

    private Dictionary<ePlayerState, FsmState<Player>> stateByEnum;

    public InputManager input => GameManager.Instance.input;

    public Animator anim;

    private void Reset()
    {
        stats = new PlayerStats();
        stats.Set(
            transform,
            transform.GetChild(0).GetComponent<Watergun>());
    }

    private void Awake()
    {
        stats.Set(
            transform, 
            transform.GetChild(0).GetComponent<Watergun>());
        physics = new PlayerPhysics(stats.physicsStat);
        anim = transform.GetComponent<Animator>();

        stateByEnum = new Dictionary<ePlayerState, FsmState<Player>>();
        stateByEnum.Add(ePlayerState.Idle , new PlayerIdle());
        stateByEnum.Add(ePlayerState.Move , new PlayerMove());
        stateByEnum.Add(ePlayerState.Attack , new PlayerAttack());
        stateByEnum.Add(ePlayerState.MovingAttack , new PlayerMovingAttack());

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
        physics.PhysicsUpdate();
    }

    public void ChangeState(ePlayerState state)
    {
        StateMachine.ChangeState(stateByEnum[state]);
    }

    public void StateRevert()
    {
        StateMachine.StateRevert();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        stats.physicsStat.EvauateCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        stats.physicsStat.EvauateCollision(collision);
    }

    
}
