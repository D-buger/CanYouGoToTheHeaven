using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAcceleration = 1f;
    Vector3 velocity, desiredVelocity;
    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
    [SerializeField]
    bool activateAllowedArea;
    [SerializeField, Min(0)]
    int maxAirJumps = 0;
    [SerializeField]
    float currentSpeed = 0f;

    Rigidbody2D body;

    bool desiredJump;
    bool onGround;
    int jumpPhase;
    int shotPhase = 0;

    [SerializeField]
    bool isShot;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = 0;
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Debug.Log(playerInput.x);

        desiredVelocity = playerInput * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");

        isShot |= Input.GetKeyDown(KeyCode.Z);

        //Debug.Log(velocity.y);
    }

    private void FixedUpdate()
    {
        UpdateState();
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        if (activateAllowedArea)
        {
            if (body.position.x < allowedArea.xMin)
            {
                body.position = new Vector2(allowedArea.xMin, body.position.y);
                velocity.x = 0f;
            }
            if (body.position.x > allowedArea.xMax)
            {
                body.position = new Vector2(allowedArea.xMax, body.position.y);
                velocity.x = 0f;
            }
            if (body.position.y < allowedArea.yMin)
            {
                body.position = new Vector2(body.position.x, allowedArea.yMin);
                velocity.y = 0f;
            }
            if (body.position.y > allowedArea.yMax)
            {
                body.position = new Vector2(body.position.x, allowedArea.yMax);
                velocity.y = 0f;
            }
        }
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        if (isShot)
        {
            isShot = false;
            TestShot();
        }

        GetSpeed();

        body.velocity = velocity;
        onGround = false;
    }

    Vector3 lastPosition;
    void GetSpeed()
    {
        currentSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }

    void UpdateState()
    {
        velocity = body.velocity;
        if (onGround)
        {
            jumpPhase = 0;
        }
    }

    void Jump()
    {
        Debug.Log(velocity.y);
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
            Debug.Log(velocity.y);
        }
    }

    void TestShot()
    {
        shotPhase++;
        float rebound = Mathf.Sqrt( -1f * Physics2D.gravity.y * 1f / shotPhase);
        velocity.y = rebound;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
