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
    
    [SerializeField] GameObject[] WeaponsCarried;
    Gun currentGun;

    PlayerUI HUD;

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
        OnFoot.PrimaryFire.performed += ctx => PrimaryFire();
        OnFoot.SecondaryFire.performed += ctx => SecondaryFire();

        ToggleMouseCursor(false);

        #region Weapon
        // Setup weapons
        maxWeaponIndex = WeaponsCarried.Length - 1;

        // Update the UI with info about the first weapon carried. 
        HUD = GetComponent<PlayerUI>();
        currentGun = WeaponsCarried[0].GetComponent<Gun>();
        HUD.UpdateWeaponText(currentGun.WeaponName, currentGun.MaxAmmo, currentGun.MaxAmmo); // Inital HUD update

        // Set all other weapons carried to inactive. 
        for (int i = 0; i < WeaponsCarried.Length; i++)
        {
            if (i != 0)
            {
                WeaponsCarried[i].SetActive(false);
            }
        }

        #endregion Weapon
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

                // Equip the now-current weapon. 
                Debug.Log($"Previous: {previousWeaponIndex}, Current: {currentWeaponIndex}");
                WeaponsCarried[currentWeaponIndex].SetActive(true);
                currentGun = WeaponsCarried[currentWeaponIndex].GetComponent<Gun>();
                HUD.UpdateWeaponText(currentGun.WeaponName, currentGun.AmmoInClip, currentGun.MaxAmmo);
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

                // Equip the now-current weapon. 
                Debug.Log($"Previous: {previousWeaponIndex}, Current: {currentWeaponIndex}");
                WeaponsCarried[currentWeaponIndex].SetActive(true);
                currentGun = WeaponsCarried[currentWeaponIndex].GetComponent<Gun>();
                HUD.UpdateWeaponText(currentGun.WeaponName, currentGun.AmmoInClip, currentGun.MaxAmmo);
            }
        }
    }

    void QuickSwitchWeapons()
    {
        Debug.Log("Quick Switch");
        if (WeaponsCarried.Length > 1)
        {
            int tempPrevious = previousWeaponIndex;
            int tempCurrent = currentWeaponIndex;
            currentWeaponIndex = tempPrevious;
            previousWeaponIndex = tempCurrent;
            WeaponsCarried[previousWeaponIndex].SetActive(false);
            WeaponsCarried[currentWeaponIndex].SetActive(true);
            currentGun = WeaponsCarried[currentWeaponIndex].GetComponent<Gun>();
            HUD.UpdateWeaponText(currentGun.WeaponName, currentGun.AmmoInClip, currentGun.MaxAmmo);
        }
    }
    
    /// <summary>
    /// Returns true if able to refill the ammo of the weapon. 
    /// Returns false if ammo is full. 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool PickUpAmmo(int amount)
    {
        if (!currentGun.isFull)
        {
            currentGun.RefillAmmo(amount);
            Debug.Log($"Picking up ammo: {amount}");
            HUD.UpdateAmmo(currentGun.AmmoInClip);
            return true;
        }
        else
        {
            Debug.Log("Currently held weapon has full ammo. ");
            return false;
        }
    }

    void PrimaryFire()
    {
        //Debug.Log("Primary Fire");
        currentGun.FireGun();
        // Update the UI to show the decreased ammo count. 
        HUD.UpdateAmmo(currentGun.AmmoInClip);
    }

    void SecondaryFire()
    {
        Debug.Log("Secondary Fire");
    }
}
