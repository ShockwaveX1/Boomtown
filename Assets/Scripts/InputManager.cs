using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions OnFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    bool isPaused;

    int currentWeaponIndex;

    void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // ctx = callback context
        OnFoot.Jump.performed += ctx => motor.Jump();
        OnFoot.Crouch.performed += ctx => motor.Crouch();
        OnFoot.Sprint.performed += ctx => motor.Sprint();

        ToggleMouseCursor(false);
    }

    void FixedUpdate()
    {
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());        
    }

    private void LateUpdate()
    {
        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
        SwitchWeapons(OnFoot.WeaponSwitch.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }

    void SwitchWeapons(Vector2 input)
    {
        if (input.y > 0 || input.y < 0)
            Debug.Log(input);
    }

    void ToggleMouseCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;
    }
}
