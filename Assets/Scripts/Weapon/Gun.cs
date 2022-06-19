/* TEST WEAPON */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon //MonoBehaviour
{
    public string WeaponName = "Put a weapon name in the inspector. ";

    public int MaxAmmo = 25;
    public int AmmoInClip { get; private set; }

    public int AmmoPerShot = 1;

    public bool isFull { get; private set; }

    private void Awake()
    {
        AmmoInClip = MaxAmmo;
        isFull = true;
    }

    public void RefillAmmo(int refillAmount)
    {
        AmmoInClip += refillAmount;
        if (AmmoInClip > MaxAmmo)
        {
            AmmoInClip = MaxAmmo;
        }
    }

    public void FireGun()
    {
        if (AmmoInClip >= AmmoPerShot)
        {
            AmmoInClip -= AmmoPerShot;
            isFull = false;
        }
        else
        {
            Debug.Log("Out of Ammo");
        }
    }

    public void AltFireGun()
    {
        Debug.Log("Alt Fire");
    }

    public void Reload()
    {
        Debug.Log("Reloading");
    }
}
