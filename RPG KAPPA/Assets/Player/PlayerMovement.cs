using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.6f;
    public LayerMask groundMask;
    public bool isGrounded;
    public Animator anim;
    public bool canMove = true;

    //Battle

    private void Update()
    {
        Movement();

    }
    public void Movement()
    {
        if (canMove)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                //anim.SetBool("Jump", false);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //anim.SetBool("Jump", true);
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (move.x > 0.01f || move.z > 0.01)
            {
                //anim.SetFloat("Speed", 1);
            }
            else
            {
                //anim.SetFloat("Speed", 0);
            }
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

    }
}
