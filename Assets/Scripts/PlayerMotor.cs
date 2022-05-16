using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float Speed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    // Receive inputs from InputManager.cs and apply them to our character controller. 
    public void ProcessMove(Vector2 input)
    {        
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y; // Taking the 2D vector of input and applying it to the 3D vector of the character. 
        controller.Move(transform.TransformDirection(moveDirection) * Speed * Time.deltaTime);
    }
}
