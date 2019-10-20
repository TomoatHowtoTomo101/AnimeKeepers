using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class Stickman : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idle, run, prejump, midjump, postjump, prefall, midfall, postfall;

    private CharacterController controller;
    private Vector3 velocity, gravity;
    private float moveSpeed, turnSpeed, jumpSpeed, fallSpeed, drag;
    private float vInput, hInput;
    private bool spaceKey, jumping;

    // Start is called before the first frame update
    void Start()
    {
        animancer.Play(idle);
        controller = gameObject.GetComponent<CharacterController>();
        velocity = Vector3.zero;
        gravity = new Vector3(0f, -3.3f, 0f);
        moveSpeed = 3f;
        turnSpeed = 100.0f;
        jumpSpeed = 150.0f;
        fallSpeed = -4f;
        drag = 0.85f;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity *= drag;
        velocity += gravity;
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        spaceKey = Input.GetKey("space");

        float scaleMoveSpeed = 1f;

        if (controller.isGrounded && !jumping)
        {
            GroundAnimation();
            if (spaceKey) Jump();
        }
        else if (jumping && velocity.y > fallSpeed) velocity.y += 3f;
        if (vInput < 0) scaleMoveSpeed = 0.4f;

        velocity += transform.forward * Input.GetAxis("Vertical") * moveSpeed * scaleMoveSpeed;
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(velocity.y);
        transform.Rotate(0, hInput * turnSpeed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        jumping = true;
        animancer.Play(prejump).OnEnd = MidJump;
    }
    private void MidJump()
    {
        velocity.y = jumpSpeed;
        animancer.Play(midjump).OnEnd = PostJump;
    }
    private void PostJump()
    {
        if (controller.isGrounded) PostFall();
        else if (velocity.y < fallSpeed) PreFall();
        else animancer.Play(postjump).OnEnd = PostJump;
    }
    private void PreFall()
    {
        if (controller.isGrounded) PostFall();
        else animancer.Play(prefall).OnEnd = MidFall;
    }
    private void MidFall()
    {
        if (controller.isGrounded) PostFall();
        else animancer.Play(midfall).OnEnd = MidFall;
    }
    private void PostFall()
    {
        jumping = false;
        animancer.Play(postfall).OnEnd = GroundAnimation;
    }
    private void GroundAnimation()
    {
        if (vInput > 0 || vInput < 0)
        {
            if (!animancer.IsPlaying(run))
                animancer.CrossFade(run);
        }
        else if (!animancer.IsPlaying(idle))
            animancer.CrossFade(idle);
    }
}
