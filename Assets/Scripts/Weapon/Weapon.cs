using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //public WeaponType WeaponType = WeaponType.RANGED;



}

public enum WeaponType
{
    MELEE,
    RANGED,
    THROWN, // Example: grenates
    REMOTE // Example: TNT
}
