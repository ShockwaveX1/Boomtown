/* This script controls the movement of the player. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController CharController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float Speed = 5f;
    public float gravity = -9.8f; // Gravity will eventually be moved to Game Rules
    public float jumpHeight = 1.5f;
    bool lerpCrouch;
    bool crouching = false;
    float crouchTimer = 0.0f;
    bool sprinting;

    void Start()
    {
        CharController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = CharController.isGrounded;
        // Crouching
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                CharController.height = Mathf.Lerp(CharController.height, 1, p);
            else
                CharController.height = Mathf.Lerp(CharController.height, 2, p);

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
        CharController.Move(transform.TransformDirection(moveDirection) * Speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        CharController.Move(playerVelocity * Time.deltaTime);
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
