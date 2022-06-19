using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public bool DoesRespawn = true;
    public float RespawnTime = 10f;

    public abstract int GetPickUpValue();

    public virtual void PickThisUp()
    {
        Debug.Log("Parent");
        if (DoesRespawn)
        {
            Debug.Log("This pickup will respawn");
            StartCoroutine(DeactivePickUp());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Pickups that respawn become invisible and unable to be picked up for a period of time. 
    /// </summary>
    IEnumerator DeactivePickUp()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;        

        yield return new WaitForSeconds(RespawnTime);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
