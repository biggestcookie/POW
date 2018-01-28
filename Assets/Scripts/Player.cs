using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;
    public float moveSpeed;
    public float fireRate;

    float nextFire;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public bool direction;
    private Animator animator;
    public Vector2 directionalInput;
    Vector3 velocity;

    Controller2D controller2D;
    private void Start()
    {
        controller2D = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        direction = true;
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        CalculateVelocity();
        controller2D.Move(velocity * Time.deltaTime);
        if (controller2D.collisions.above || controller2D.collisions.below)
            velocity.y = 0;
        if ((velocity.x > 0 && !direction) || (velocity.x < 0 && direction))
            FlipSprite();
        animator.SetBool("inAir", !controller2D.collisions.below);
        animator.SetFloat("speed", Mathf.Abs(velocity.x));
    }
    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }
    public void SetActive(bool x)
    {
        animator.SetBool("active", x);
    }

    public void OnJumpInputDown()
    {
        if (controller2D.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }
    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }
    public void OnAbilityInputDown()
    {
        animator.SetTrigger("ability");
    }

    /*public void OnMousePress()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
                Instantiate(shot, gun.position, gun.rotation);
            //GetComponent<AudioSource>().Play();
        }
    }*/
    private void FlipSprite()
    {
        direction = !direction;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, controller2D.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;

    }
}