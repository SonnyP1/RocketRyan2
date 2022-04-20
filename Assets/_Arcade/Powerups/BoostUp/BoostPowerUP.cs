using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<PlayerMovementComponent>().AddBoost(10f);
            Destroy(gameObject);
        }
    }
}
