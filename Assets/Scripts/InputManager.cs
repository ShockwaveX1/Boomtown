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

    int currentWeaponIndex = 0; // Cannot be negative. 
    int previousWeaponIndex = 0; // The index of the previous weapon used. Used for quick switching. 
    int maxWeaponIndex; // The highest index that the player can go to before returning to 0.     
    
    [SerializeField] 
    GameObject[] WeaponsCarried;

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

        OnFoot.QuickSwitch.performed += ctx => QuickSwitchWeapons();

        ToggleMouseCursor(false);

        maxWeaponIndex = WeaponsCarried.Length - 1;
        // Set all other weapons carried to inactive. 
        for (int i = 0; i < WeaponsCarried.Length; i++)
        {
            if (i != 0)
            {
                WeaponsCarried[i].SetActive(false);
            }
        }
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

    void ToggleMouseCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;
    }

    void SwitchWeapons(Vector2 input)
    {
        if (input.y > 0) // Previous Weapon
        {
            if (WeaponsCarried.Length > 1) // Make sure that there is a weapon to switch to. 
            {
                WeaponsCarried[currentWeaponIndex].SetActive(false); // Remove the old weapon. 
                previousWeaponIndex = currentWeaponIndex; // Store the old weapon index. 

                if (currentWeaponIndex == 0)
                {
                    // Cycle back around to the last index. 
                    currentWeaponIndex = maxWeaponIndex;
                }
                else
                {
                    currentWeaponIndex--;
                }

                // Equip the nowcurrent weapon. 
                Debug.Log($"Previous: {previousWeaponIndex}, Current: {currentWeaponIndex}");
                WeaponsCarried[currentWeaponIndex].SetActive(true);
            }
        }
        else if (input.y < 0) // Next Weapon
        {
            if (WeaponsCarried.Length > 1) // Make sure that there is a weapon to switch to. 
            {
                WeaponsCarried[currentWeaponIndex].SetActive(false); // Remove the old weapon. 
                previousWeaponIndex = currentWeaponIndex; // Store the old weapon index. 

                if (currentWeaponIndex == maxWeaponIndex)
                {
                    // Cycle back around to the first index. 
                    currentWeaponIndex = 0;
                }
                else
                {
                    currentWeaponIndex++;
                }

                // Equip the nowcurrent weapon. 
                Debug.Log($"Previous: {previousWeaponIndex}, Current: {currentWeaponIndex}");
                WeaponsCarried[currentWeaponIndex].SetActive(true);
            }
        }
    }

    void QuickSwitchWeapons()
    {
        Debug.Log("Quick Switch");
        int tempPrevious = previousWeaponIndex;
        int tempCurrent = currentWeaponIndex;
        currentWeaponIndex = tempPrevious;
        previousWeaponIndex = tempCurrent;
        WeaponsCarried[previousWeaponIndex].SetActive(false);
        WeaponsCarried[currentWeaponIndex].SetActive(true);
    }

    void PrimaryFire()
    {
        Debug.Log("Primary Fire");
    }

    void SecondaryFire()
    {
        Debug.Log("Secondary Fire");
    }
}
