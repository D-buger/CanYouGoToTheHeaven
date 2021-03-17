using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    Vector3 velocity, desiredVelocity;
    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    Rigidbody2D body;

    bool desiredJump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = playerInput * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y =
            Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);

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
    }

    void Jump()
    {
        velocity.y += 5f;
    }
}
