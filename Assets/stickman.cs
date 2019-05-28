using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickman : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 moveDir = Vector3.zero;
    public float moveSpeed = 10.0f;
    public float turnSpeed = 360.0f;
    public float gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
            animator.SetInteger("Animation", 1);
        else animator.SetInteger("Animation", 0);

        if (controller.isGrounded)
            moveDir = transform.forward * Input.GetAxis("Vertical") * moveSpeed;

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDir * Time.deltaTime);
        moveDir.y -= gravity * Time.deltaTime;
        Debug.Log(turn + " * " + turnSpeed + " * " + Time.deltaTime + " = " + turn * turnSpeed * Time.deltaTime);
    }
}
