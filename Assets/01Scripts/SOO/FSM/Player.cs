using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Idle,
    Move,
    Attack,
}

public class Player : MonoBehaviour
{
    public StateMachine<Player> StateMachine { get; private set; }
    public PlayerPhysics physics;
    public PlayerStats stats;

    public Watergun watergun;

    private Dictionary<ePlayerState, FsmState<Player>> stateByEnum;

    public InputManager input => GameManager.Instance.input;

    public Animator anim;

    private void Awake()
    {
        stats.Set(transform, transform.GetComponent<Rigidbody2D>(), transform.position.y);
        physics = new PlayerPhysics(stats);
        watergun = transform.GetChild(0).GetComponent<Watergun>();
        anim = transform.GetComponent<Animator>();

        stateByEnum = new Dictionary<ePlayerState, FsmState<Player>>();
        stateByEnum.Add(ePlayerState.Idle , new PlayerIdle());
        stateByEnum.Add(ePlayerState.Move , new PlayerMove());
        stateByEnum.Add(ePlayerState.Attack , new PlayerAttack());


        StateMachine = new StateMachine<Player>();
        StateMachine.Init(this, new PlayerIdle());
    }
    
    private void Update()
    {
        StateMachine.Update();

        anim.SetBool("isAttack", input.behaviourActive);

        if (input.behaviourActive)
            physics.TestShot();
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
        stats.EvauateCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        stats.EvauateCollision(collision);
    }

    
}
