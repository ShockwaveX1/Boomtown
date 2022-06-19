using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public bool DoesRespawn;

    public abstract int GetPickUpValue();

    public abstract void PickThisUp();
}
