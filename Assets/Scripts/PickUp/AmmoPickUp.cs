using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : PickUp
{
    int AmmoRefillAmount = 10;

    private void Start()
    {
        DoesRespawn = false;
    }

    public override int GetPickUpValue()
    {
        return AmmoRefillAmount;
    }

    public override void PickThisUp()
    {
        Debug.Log("Refilling Ammo");
        if (DoesRespawn)
        {
            GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("This pickup will respawn");
        }
        else
        {
            Destroy(gameObject);
        }
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
