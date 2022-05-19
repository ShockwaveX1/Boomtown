/* This script controls the movement of the player. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float Speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    bool lerpCrouch;
    bool crouching = false;
    float crouchTimer = 0.0f;
    bool sprinting;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }

    // Receive inputs from InputManager.cs and apply them to our character controller. 
    public void ProcessMove(Vector2 input)
    {        
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y; // Taking the 2D vector of input and applying it to the 3D vector of the character. 
        controller.Move(transform.TransformDirection(moveDirection) * Speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            Speed = 8;
        else
            Speed = 5;
    }
}
