using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] new Transform camera;
    [SerializeField] Transform groundcheck;
    [SerializeField] Animator animator;
    [Space]

    [SerializeField] LayerMask groundMask;
    [Space]

    [SerializeField] float rotationSpeedVelocity;
    [Space]

    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] float speed = 6f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [Space]

    bool isGrounded;
    [Space]

    Vector3 velocity;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
    }

    void Update()
    {
        Gravity();

        Vector3 direction = GetDirection();

        AnimateRun(direction);

        Move(direction);

    }



    // Support
    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    Vector3 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }

    void AnimateRun(Vector3 direction)
    {
        if (direction != Vector3.zero)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);
    }

    void Move(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeedVelocity, rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

    }

}


// Comments
/*
I decided to use GetAxisRaw, because I would like to use the WASD controls with the mouse or a Controller maybe in the future. 

First problem is that the character won't use local directions instead of world space directions to move, so the third person control feels off in a 3D space:
- Quaternion targetRotation = Quaternion.LookRotation(direction + cam.eulerAngles.y); = error so I use stg different that I found on youtube
- transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

Because it seems easier and more convinient to use the unity built in controller and correct it with scripts I decided to use it. That's why I use the following:
- controller.Move(direction * speed * Time.deltaTime);


*/



