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
    [SerializeField, Min(0)]
    int maxAirJumps = 0;

    Rigidbody2D body;

    bool desiredJump;
    bool onGround;
    int jumpPhase;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = 0;
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = playerInput * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        UpdateState();
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

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

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        body.velocity = velocity;
        onGround = false;
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
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
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
