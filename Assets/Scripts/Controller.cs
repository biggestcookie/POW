using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsComponent))]
public class Controller : MonoBehaviour
{
    bool CanMove = true;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    PhysicsComponent PhysicsComponentInst;

    void Start()
    {
        PhysicsComponentInst = GetComponent<PhysicsComponent>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (PhysicsComponentInst.collisions.above || PhysicsComponentInst.collisions.below)
        {
            velocity.y = 0;
        }
        if (this.GetComponent<ControllingObject>() != null && CanMove == true)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && PhysicsComponentInst.collisions.below)
            {
                velocity.y = jumpVelocity;
            }

            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (PhysicsComponentInst.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            PhysicsComponentInst.Move(velocity * Time.deltaTime);
        }
    }
}