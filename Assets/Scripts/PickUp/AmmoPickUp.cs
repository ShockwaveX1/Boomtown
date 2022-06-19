using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : PickUp
{
    int AmmoRefillAmount = 10;

    private void Start()
    {
        
    }

    public override int GetPickUpValue()
    {
        return AmmoRefillAmount;
    }

    public override void PickThisUp()
    {
        base.PickThisUp();
        Debug.Log("Refilling Ammo");
    }

    private void OnTriggerEnter(Collider other)
    {
        InputManager inputManager = other.GetComponent<InputManager>();        
        if (inputManager.PickUpAmmo(AmmoRefillAmount))
        {
            PickThisUp();
        }
    }
}
