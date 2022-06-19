/* Right now, this script will test health. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Health;
    float MaxHealth = 50;

    private void Start()
    {
        Health = MaxHealth;
    }
}
