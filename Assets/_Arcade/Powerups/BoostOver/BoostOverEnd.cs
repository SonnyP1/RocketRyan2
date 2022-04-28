using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOverEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.GetComponent<HealthComponent>())
            {
                return;
            }
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<CharacterController>().enabled = true;
            other.GetComponent<PlayerMovementComponent>().enabled = true;
        }
    }
}
